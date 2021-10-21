using AuditoriaAPI.Infrasctructure;
using CQRS.Core.Application.Commands;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuditoriaAPI.Domain;

namespace AuditoriaAPI.Application.NovaAuditoriaCommand
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
                var auditoria = new Auditoria
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
