using ThirdEye.Back.Services.Abstractions;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace ThirdEye.Back.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            var addressFrom = new MailboxAddress(_configuration["EmailConfiguration:UserName"],
                                                 _configuration["EmailConfiguration:FromAddress"]);
            var addressTo = new MailboxAddress(string.Empty,
                                               email);

            emailMessage.From.Add(addressFrom);
            emailMessage.To.Add(addressTo);
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message,
            };

            using var client = new SmtpClient();

            string smtpServer = _configuration["EmailConfiguration:SmtpServer"];
            int port = int.Parse(_configuration["EmailConfiguration:TlsPort"]);

            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            await client.ConnectAsync(smtpServer,
                                      port,
                                      SecureSocketOptions.StartTls);

            client.Authenticate(addressFrom.Address,
                                _configuration["EmailConfiguration:Password"]);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }
}
