using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchool.Utility
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message, List<string> multipleCCmail = null);
    }
    public class EmailSender : IEmailSender
    {
        public EmailSettings _emailSettings { get; }
        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }


        public Task SendEmailAsync(string email, string subject, string message, List<string> multipleCCmail = null)
        {

            Execute(email, subject, message, multipleCCmail).Wait();
            return Task.FromResult(0);
        }

        public async Task Execute(string toEmail, string subject, string message, List<string> multipleCCmail)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                    Priority = MailPriority.High,
                };
                mail.To.Add(new MailAddress(toEmail));
                mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

                if (multipleCCmail != null)
                {
                    foreach (string emailAddress in multipleCCmail)
                        mail.Bcc.Add(new MailAddress(emailAddress));
                }

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);

                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
