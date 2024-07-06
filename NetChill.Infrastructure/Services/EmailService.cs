using NetChill.Application.DTOs.Email;
using NetChill.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using NetChill.Application.Abstractions;

namespace NetChill.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public EmailValues _emailConfig { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<EmailValues> mailConfig, ILogger<EmailService> logger)
        {
            _emailConfig = mailConfig.Value;
            _logger = logger;
        }


        public async Task SendEmailAsync(EmailRequestDto emailRequestDto)
        {
            try
            {
                MimeMessage email = new MimeMessage();

                email.Sender = new MailboxAddress(_emailConfig.DisplayName, _emailConfig.EmailFrom);
                email.To.Add(MailboxAddress.Parse(emailRequestDto.EmailTo));
                email.Subject = emailRequestDto.Subject;

                BodyBuilder builder = new BodyBuilder();
                builder.HtmlBody = emailRequestDto.Body;
                email.Body = builder.ToMessageBody();

                SmtpClient client = new SmtpClient();
                client.Connect(_emailConfig.SmtpHost, _emailConfig.SmtpPort, SecureSocketOptions.StartTls);
                client.Authenticate(_emailConfig.EmailFrom, _emailConfig.AuthPassword);

                await client.SendAsync(email);
                client.Disconnect(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception(ex.Message);
            }
        }
    }
}
