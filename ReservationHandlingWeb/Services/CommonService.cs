
using ReservationHandlingWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace ReservationHandlingWeb.Services
{
    public class CommonService
    {
        private readonly ReservationDBContext _context;
        public CommonService(ReservationDBContext context)
        {
            _context = context;
        }

        public void MailSend(string toAdd, int ID = -1, string MsgType = "")
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("gayanwtesting@gmail.com", "PassForTesting"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("gayanwtesting@gmail.com"),
                Subject = "test",
                Body = "<h1>ggwp</h1>",
                IsBodyHtml = true,
            };


            if (MsgType == "CSV")
            {
                mailMessage.Subject = "Member Details";
                mailMessage.Body = "<p>Please fill the attached .csv file and upload through following link.<br><a href='https://localhost:44301/User'>Link</a><br>Event ID - " + ID + "<br>Seating plan also attached<br><br> " +
                    "Best Regurds,<br>BMICH Reservation Handling.</p>";
            }
            mailMessage.To.Add(toAdd);

            var attachment = new Attachment(@"wwwroot\Resources\MemberDetails.csv", new ContentType("text/csv"));
            var attachment2 = new Attachment(@"wwwroot\Resources\SeatingPlan.png", new ContentType("image/png"));
            mailMessage.Attachments.Add(attachment);
            mailMessage.Attachments.Add(attachment2);

            smtpClient.Send(mailMessage);

        }

        //private string MailMsg(string MsgType)
        //{
        //    switch (MsgType)
        //    {
        //        case "CSV":
        //            return "";
        //    }
        //}
    }
}
