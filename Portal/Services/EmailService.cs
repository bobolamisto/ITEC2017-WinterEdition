using Microsoft.Extensions.Options;
using Portal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Portal.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailService : IEmailService
    {
        public void SendEmail(string toEmail, string subject, string message)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("portalitec2017@gmail.com");
            msg.To.Add(toEmail);
            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            System.Net.NetworkCredential netc = new NetworkCredential();
            netc.UserName = "portalitec2017@gmail.com";
            netc.Password = "123456789?";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = netc;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(msg);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

}
