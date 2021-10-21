using AuditoriaAPI.Infrasctructure;
using CQRS.Core.Application.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

    public class NovaAuditoriaCommandInput : CommandInput<NovaAuditoriaCommandResult>
    {
        public IEnumerable<NovaAuditoriaCommandInputItem> Auditorias { get; init; }
    }

    public class NovaAuditoriaCommandInputItem
    {
        public NovaAuditoriaCommandInputItem(
            EntityState tipoAuditoria,
            Guid? idEntidade,
            string nomeEntidade,
            string nomeTabela,
            ICollection<NovaAuditoriaPropriedadeCommandInputItem> propriedades)
        {
            TipoAuditoria = tipoAuditoria;
            IdEntidade = idEntidade;
            NomeEntidade = nomeEntidade;
            NomeTabela = nomeTabela;
            Propriedades = propriedades;
        }

        public EntityState TipoAuditoria { get; set; }

        public Guid? IdEntidade { get; set; }

        public string NomeEntidade { get; set; }

        public string NomeTabela { get; set; }

        public ICollection<NovaAuditoriaPropriedadeCommandInputItem> Propriedades { get; set; }
    }

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

    public class NovaAuditoriaCommandResult : CommandResult
    {

    }
}
