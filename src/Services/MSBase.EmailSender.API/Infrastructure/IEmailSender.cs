using MSBase.EmailSender.Templates.Views.Emails;

namespace MSBase.EmailSender.API.Infrastructure;

public interface IEmailSender
{
    Task<bool> SendEmail<T>(T model) where T : EmailBaseViewModel;
}