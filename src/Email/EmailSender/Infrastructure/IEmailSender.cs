using EmailSender.Templates.Views.Emails;

namespace EmailSender.Infrastructure
{
    public interface IEmailSender
    {
        Task<bool> SendEmail<T>(T model) where T : EmailBaseViewModel;
    }
}