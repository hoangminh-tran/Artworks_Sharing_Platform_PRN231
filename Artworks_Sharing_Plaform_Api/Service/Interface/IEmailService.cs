namespace Artworks_Sharing_Plaform_Api.Service.Interface;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}
