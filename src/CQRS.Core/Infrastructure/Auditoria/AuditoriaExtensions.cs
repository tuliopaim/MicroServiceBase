using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using MSBase.Core.Domain;

namespace MSBase.Core.Infrastructure.Auditoria
{
    public static class AuditoriaExtensions
    {
        public static AuditoriaEvent ObterEventoDeAuditoria(this ChangeTracker changeTracker)
        {
            var auditorias = changeTracker.Entries()
                .Where(e => e.State
                    is EntityState.Added
                    or EntityState.Modified
                    or EntityState.Deleted &&
                    e.Entity is Entity and not (IAuditoria or IAuditoriaPropriedade))
                .Select(MapearParaAuditoria)
                .ToList();

            return new AuditoriaEvent(auditorias);
        }

        public static NovaAuditoriaDto MapearParaAuditoria(EntityEntry entityEntry)
        {
            var auditoria = new NovaAuditoriaDto
            {
                NomeEntidade = entityEntry.Metadata.Name,
                NomeTabela = entityEntry.Metadata.GetTableName(),
                TipoAuditoria = entityEntry.State,
            };

            ExtrairAuditoriaDasPropriedades(entityEntry, auditoria);

            return auditoria;
        }

        private static void ExtrairAuditoriaDasPropriedades(EntityEntry entityEntry, NovaAuditoriaDto auditoria)
        {
            var propriedadesAntigas = entityEntry.GetDatabaseValues();

            foreach (var property in entityEntry.Properties)
            {
                var ehChavePrimaria = property.Metadata.IsPrimaryKey();
                if (ehChavePrimaria)
                {
                    auditoria.IdEntidade = new Guid(property.CurrentValue?.ToString());
                }

                if (auditoria.EhModificado && ValorDaPropriedadeNaoMudou(property, propriedadesAntigas))
                {
                    continue;
                }

                var valorAntigo = ObterValorAntigo(auditoria, propriedadesAntigas, property);
                var valorNovo = ObterValorNovo(auditoria, property);

                auditoria.Propriedades.Add(new NovaAuditoriaPropriedadeDto
                {
                    EhChavePrimaria = ehChavePrimaria,
                    ValorAntigo = valorAntigo,
                    ValorNovo = valorNovo,
                    NomeDaColuna = ObterNomeDaColuna(entityEntry, property),
                    NomeDaPropriedade = property.Metadata.Name
                });
            }
        }

        private static bool ValorDaPropriedadeNaoMudou(PropertyEntry property, PropertyValues propriedadesAntigas)
        {
            var valorAntigo = propriedadesAntigas[property.Metadata];

            return valorAntigo?.ToString() == property.CurrentValue?.ToString();
        }

        private static string ObterValorAntigo(NovaAuditoriaDto auditoria, PropertyValues propriedadesAntigas, PropertyEntry property)
        {
            return !auditoria.EhAdicionado
                ? propriedadesAntigas[property.Metadata]?.ToString()
                : null;
        }

        private static string ObterValorNovo(NovaAuditoriaDto auditoria, PropertyEntry property)
        {
            return !auditoria.EhDeletado
                ? property.CurrentValue?.ToString()
                : null;
        }

        private static string ObterNomeDaColuna(EntityEntry entityEntry, PropertyEntry property)
        {
            var storeObject = StoreObjectIdentifier.Table(
                entityEntry.Metadata.GetTableName(), entityEntry.Metadata.GetSchema());

            var nomePropriedade = property.Metadata.GetColumnName(storeObject);

            return nomePropriedade;
        }

    }
}