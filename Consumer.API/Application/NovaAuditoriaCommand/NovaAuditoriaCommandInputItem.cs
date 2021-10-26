﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AuditoriaAPI.Application.NovaAuditoriaCommand
{
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
}