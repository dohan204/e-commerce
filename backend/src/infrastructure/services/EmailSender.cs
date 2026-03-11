using System.Net.Mail;
using application.interfaces;
using Microsoft.Extensions.Options;
using MimeKit;

namespace infrastructure.services
{
    public class EmailOptions
    {
        public string SmtpServer {get; set; } = string.Empty;
        public int SmtpPost {get; set; }
        public string SenderName {get; set; } = string.Empty;
        public string SenderEmail {get; set; } = string.Empty;
        public string UserName {get; set;} = string.Empty;
        public string Password {get; set; } = string.Empty; 
    }
    public class EmailSender : IEmailSender
    {
        private readonly EmailOptions _email; 
        public EmailSender(IOptions<EmailOptions> options)
        {
            _email = options.Value;
            Console.WriteLine(_email.SmtpServer);
            Console.WriteLine(_email.SmtpPost);
            Console.WriteLine(_email.SenderName);
            Console.WriteLine(_email.SenderEmail);
            Console.WriteLine(_email.UserName);
            Console.WriteLine(_email.Password);
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_email.SenderName, _email.SenderEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));

            message.Subject = subject;
            var builder = new BodyBuilder();
            if(isHtml)
            {
                builder.HtmlBody = body;
            } else
            {
                builder.TextBody = body;
            }

            message.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                await smtp.ConnectAsync(_email.SmtpServer, _email.SmtpPost, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_email.UserName, _email.Password);
                await smtp.SendAsync(message);
                await smtp.DisconnectAsync(true);
            }catch (Exception ex) {
                throw new InvalidOperationException($"Error sending email: {ex.Message}");
            }
        }
    }
}