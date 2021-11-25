namespace EmailSender.Templates.Views.Emails.PessoaCadastradaComSucesso;

public class PessoaCadastradaComSucessoViewModel : EmailBaseViewModel
{
    public PessoaCadastradaComSucessoViewModel(string nome, string email, string assunto) : base(email, assunto)
    {
        Nome = nome;
    }

    public string Nome { get; set; }
}
