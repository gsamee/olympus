
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
                mailMessage.Subject = "BMICH Reservation Handling";
                mailMessage.Body = "<p>Thank you very much for your reservation with BMICH. <br><br>" +
                    "Please follow the below steps to continue the event participant registration.<br>" +
                    "Event ID - " + ID +
                    "<br>1.Fill the event participant details in the attached csv file." +
                    "<br>2.Upload the csv file to BMICH reservation system through this <a href='https://localhost:44301/User'>Link</a><br><br>Note : You can refer the attached seating plan.<br><br> " +
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
