using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Net.Smtp;

namespace NálezníkASP.Services {
    public class EmailSender : IEmailSender {
        private readonly IConfiguration configuration;

        public EmailSender(IConfiguration configuration) {
            this.configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage) {
            var smtpServer = configuration["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
            var smtpUser = configuration["EmailSettings:SmtpUser"];
            var smtpPassword = configuration["EmailSettings:SmtpPassword"];

            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Nálezník", smtpUser)); 
            emailMessage.To.Add(new MailboxAddress("", email)); 
            emailMessage.Subject = subject;

            var bodyBuilder = new BodyBuilder {
                HtmlBody = htmlMessage
            };

            emailMessage.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient()) {
                await client.ConnectAsync(smtpServer, smtpPort, false);
                await client.AuthenticateAsync(smtpUser, smtpPassword);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
