namespace ThirdEye.Back.Services.Abstractions
{
    public interface IEmailSender
    {
        public abstract Task SendEmailAsync(string email, string subject, string message);
    }
}
