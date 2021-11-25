namespace EmailSender.Templates.Views.Emails;

public abstract class EmailBaseViewModel
{
    public EmailBaseViewModel(string email, string assunto)
    {
        Email = email;
        Assunto = assunto;  
    }

    public string Email { get; set; }
    public string Assunto { get; set; }
}
