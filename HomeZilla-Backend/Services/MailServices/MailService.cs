
using MailKit.Net.Smtp;
using MimeKit;

namespace Final.MailServices
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task Send(string ToAddress, string subject, string body)
        {
            
            string? SmtpServer = _configuration.GetSection("MailSettings:SmtpHost").Value;
            int Port = int.Parse(_configuration.GetSection("MailSettings:Port").Value);

            string? FromAddress = _configuration.GetSection("MailSettings:MailAddress").Value;
            string? Password = _configuration.GetSection("MailSettings:Password").Value;
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(FromAddress));
            email.To.Add(MailboxAddress.Parse(ToAddress));

            email.Subject = subject;

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(SmtpServer, Port);

                smtp.Authenticate(
                    FromAddress,
                    Password
                );

                await smtp.SendAsync(email);

                smtp.Disconnect(true);
            }
        }
    }
}
