using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Portal.Services;

namespace Portal.Services
{
    public static class EmailSenderExtensions
    {
        public static void SendEmailConfirmationAsync(this IEmailService emailSender, string email, string link)
        {
             emailSender.SendEmail(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>");
        }
    }
}
