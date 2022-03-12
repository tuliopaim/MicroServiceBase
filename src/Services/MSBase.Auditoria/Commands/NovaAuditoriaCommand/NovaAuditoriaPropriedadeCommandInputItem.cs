namespace MSBase.Auditoria.API.Commands.NovaAuditoriaCommand;

public class NovaAuditoriaPropriedadeCommandInputItem
{
    public NovaAuditoriaPropriedadeCommandInputItem(
        string nomeDaPropriedade,
        string nomeDaColuna,
        string valorAntigo,
        string valorNovo,
        bool ehChavePrimaria)
    {
        NomeDaPropriedade = nomeDaPropriedade;
        NomeDaColuna = nomeDaColuna;
        ValorAntigo = valorAntigo;
        ValorNovo = valorNovo;
        EhChavePrimaria = ehChavePrimaria;
    }

    public string NomeDaPropriedade { get; set; }
    public string NomeDaColuna { get; set; }
    public string ValorAntigo { get; set; }
    public string ValorNovo { get; set; }
    public bool EhChavePrimaria { get; set; }
}