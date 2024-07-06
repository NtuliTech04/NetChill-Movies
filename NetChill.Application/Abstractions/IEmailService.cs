using NetChill.Application.DTOs.Email;

namespace NetChill.Application.Abstractions
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequestDto request);
    }
}
