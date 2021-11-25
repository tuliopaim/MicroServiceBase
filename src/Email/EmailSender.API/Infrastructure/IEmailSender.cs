using EmailSender.Templates.Views.Emails;

namespace EmailSender.API.Infrastructure
{
    public interface IEmailSender
    {
        Task<bool> SendEmail<T>(T model) where T : EmailBaseViewModel;
    }
}