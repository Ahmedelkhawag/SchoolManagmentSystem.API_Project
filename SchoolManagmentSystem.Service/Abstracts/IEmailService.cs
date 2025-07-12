namespace SchoolManagmentSystem.Service.Abstracts
{
    public interface IEmailService
    {
        Task<string> SendEmailAsync(string ToEmail, string ToName, string HTMLMessage, string Subject);
    }
}
