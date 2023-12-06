using NetChill.Application.DTOs.Email;

namespace NetChill.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailRequestDto request);
    }
}
