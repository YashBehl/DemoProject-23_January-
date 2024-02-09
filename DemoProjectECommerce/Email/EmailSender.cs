using System.Net.Mail;
using System.Net;

namespace DemoProjectECommerce.Email
{
    public class EmailSender
    {
        public Task SendMails(string email, string subject, string body)
        {
            try
            {
                string fromMail = "yash.behl@successive.tech";
                string fromPassword = "klwr zwsc zzhm ruah";

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = subject;
                message.To.Add(new MailAddress(email));
                message.Body = body;
                message.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true,
                };
                smtpClient.Send(message);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw;
            }

        }

        
    }
}
