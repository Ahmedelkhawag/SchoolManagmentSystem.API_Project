using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using SchoolManagmentSystem.Data.Helpers;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Service.Implmentations
{
    public class EmailService : IEmailService
    {
        #region Fields
        private readonly MailSettings _mailSettings;
        #endregion

        #region ctor

        public EmailService(IOptions<MailSettings> options)
        {
            _mailSettings = options.Value;
        }
        #endregion


        #region Implementations

        #endregion
        public async Task<string> SendEmailAsync(string ToEmail, string ToName, string HTMLMessage, string Subject)
        {

            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
            mailMessage.To.Add(new MailboxAddress(ToName, ToEmail));
            mailMessage.Subject = Subject;
            mailMessage.Body = new TextPart("html") { Text = HTMLMessage };
            using (var client = new SmtpClient())
                try
                {
                    await client.ConnectAsync(_mailSettings.Host, _mailSettings.Port);
                    await client.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
                    await client.SendAsync(mailMessage);
                    return "Email sent successfully";
                }
                catch (Exception ex)
                {

                    throw new InvalidOperationException($"Error sending email: {ex.Message}", ex);
                }
                finally
                {
                    await client.DisconnectAsync(true);

                }
        }
    }
}
