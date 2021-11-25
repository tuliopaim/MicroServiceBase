using Auditoria.API.Domain;
using Auditoria.API.Infrasctructure;
using Core.Application.Commands;

namespace Auditoria.API.Application.NovaAuditoriaCommand
{
    public class NovaAuditoriaCommandHandler : ICommandHandler<NovaAuditoriaCommandInput, NovaAuditoriaCommandResult>
    {
        private readonly IAuditoriaRepository _auditoriaRepository;

        public NovaAuditoriaCommandHandler(IAuditoriaRepository auditoriaRepository)
        {
            _auditoriaRepository = auditoriaRepository;
        }

        public async Task<NovaAuditoriaCommandResult> Handle(NovaAuditoriaCommandInput command, CancellationToken cancellationToken)
        {
            foreach (var auditoriaCommand in command.Auditorias)
            {
                var auditoria = new Domain.Auditoria
                {
                    IdEntidade = auditoriaCommand.IdEntidade,
                    NomeEntidade = auditoriaCommand.NomeEntidade,
                    NomeTabela = auditoriaCommand.NomeTabela,
                    TipoAuditoria = auditoriaCommand.TipoAuditoria,
                    Propriedades = auditoriaCommand.Propriedades?.Select(c => new AuditoriaPropriedade
                    {
                        EhChavePrimaria = c.EhChavePrimaria,
                        NomeDaColuna = c.NomeDaColuna,
                        NomeDaPropriedade = c.NomeDaPropriedade,
                        ValorAntigo = c.ValorAntigo,
                        ValorNovo = c.ValorNovo,
                    })?.ToList()
                };

                _auditoriaRepository.Add(auditoria);
            }

            await _auditoriaRepository.UnitOfWork.CommitAsync(cancellationToken);

            return new NovaAuditoriaCommandResult();
        }
    }
}
