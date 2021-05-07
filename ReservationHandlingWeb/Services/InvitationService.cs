
using Microsoft.EntityFrameworkCore;
using QRCoder;
using ReservationHandlingWeb.Data;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ReservationHandlingWeb.Services
{
    public class InvitationService
    {
        private readonly ReservationDBContext _context;
        public InvitationService(ReservationDBContext context)
        {
            _context = context;
        }

        public async Task SendInvitation(bool IsRemind = false)
        {
            try
            {
                var list = await _context.UserMain.Where(x => x.EventDate >= DateTime.Now.Date).
                Include(x => x.HallDetail).Include(x => x.EventType).
                Include(x => x.EventSetup).Include(x => x.MemberDetailsList).ToListAsync();

                //var list = await _context.UserMain.Where(x => x.InviteDate <= DateTime.Now.Date).
                //Include(x => x.HallDetail).Include(x => x.EventType).
                //Include(x => x.EventSetup).Include(x => x.MemberDetailsList).ToListAsync();

                //.Where(z => z.IsInvitationSent == false)

                //var a = await _context.UserMain.Where(x => x.InviteDate <= DateTime.Now.Date).ToListAsync();



                var x = list;
                foreach (var evnt in list)
                {
                    if (evnt.MemberDetailsList != null)
                    {
                        foreach (var member in evnt.MemberDetailsList)
                        {
                            if (member.Email != null && member.Email != "")
                            {


                                bool state;
                                if (IsRemind)
                                {
                                    state = member.IsReminderSent;
                                }
                                else
                                {
                                    state = member.IsInvitationSent;
                                }
                                if (!state)
                                {
                                    //string code = txtCode.Text;
                                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                                    //QRCodeData qrdata = qrGenerator.CreateQrCode(inputText, QRCodeGenerator.ECCLevel.Q);
                                    QRCodeData qrdata = qrGenerator.CreateQrCode(Convert.ToString(member.ID), QRCodeGenerator.ECCLevel.Q);
                                    QRCode qrCode = new QRCode(qrdata);
                                    using (Bitmap bitMap = qrCode.GetGraphic(20))
                                    {
                                        MemoryStream ms = new MemoryStream();
                                        //bitMap.Save(ms.MapPath("~/Images/qrcode.png"), ImageFormat.Png);
                                        bitMap.Save(ms, ImageFormat.Jpeg);
                                        byte[] byteImage = ms.ToArray();
                                        Image qrimg = Image.FromStream(ms);
                                        string path = @Directory.GetCurrentDirectory() + @"\QRCodeTempSave\";
                                        string imgname = "QRCode" + "-" + member.UserID + "-" + member.ID + ".Jpeg";
                                        FileInfo file = new FileInfo(path + imgname);
                                        if (!file.Exists)
                                        {
                                            qrimg.Save(path + imgname, ImageFormat.Jpeg);
                                        }

                                        string ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);

                                        MailMessage mm = new MailMessage();
                                        mm.From = new MailAddress("gayanwtesting@gmail.com");
                                        if (IsRemind)
                                        {
                                            mm.Subject = "Reminder for " + evnt.Name + " " + evnt.EventType.Type + " Event";
                                            member.IsReminderSent = true;
                                        }
                                        else
                                        {
                                            mm.Subject = "Invitation for " + evnt.Name + " " + evnt.EventType.Type + " Event";
                                            member.IsInvitationSent = true;
                                        }

                                        string mailbody = "Dear " + member.Name + ", <br/><html><body><p>Your are invited to participate the <b>" + evnt.Name + " " + evnt.EventType.Type + "</b> event on <b>" + evnt.EventDate.ToString("dddd, dd MMMM yyyy hh:mm tt") + "</b> at <b>" + evnt.HallDetail.Name + "</b> on BMICH.</p><h3>QR Code</h3><img src=\"cid:Email\"  width='200' height='200'><br><h3>Scaning steps:</h3><p style='color:DodgerBlue'>1.produce the qr code in registration desk</p><p style='color:DodgerBlue'>2.get the seat no and meal tokens printed there</p><br><p>Best Regards!</p><p>" + evnt.Name + "</p></body></html>";
                                        AlternateView AlternateView_Html = AlternateView.CreateAlternateViewFromString(mailbody, null, MediaTypeNames.Text.Html);
                                        // Create a LinkedResource object and set Image location path and Type    
                                        LinkedResource Picture1 = new LinkedResource(path + imgname, MediaTypeNames.Image.Jpeg);
                                        Picture1.ContentId = "Email";
                                        AlternateView_Html.LinkedResources.Add(Picture1);
                                        mm.AlternateViews.Add(AlternateView_Html);
                                        mm.Body = mailbody;

                                        //mm.To.Add("kasunmadhusanka7@gmail.com");
                                        mm.To.Add(Convert.ToString(member.Email));
                                        mm.IsBodyHtml = true;
                                        SmtpClient smtp = new SmtpClient();
                                        smtp.Host = "smtp.gmail.com";
                                        smtp.EnableSsl = true;
                                        NetworkCredential NetworkCred = new NetworkCredential();
                                        NetworkCred.UserName = "gayanwtesting@gmail.com";
                                        NetworkCred.Password = "PassForTesting";
                                        smtp.UseDefaultCredentials = false;
                                        smtp.Credentials = NetworkCred;
                                        smtp.Port = 587;

                                        smtp.Send(mm);
                                        _context.Update(member);
                                        await _context.SaveChangesAsync();
                                    }
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
