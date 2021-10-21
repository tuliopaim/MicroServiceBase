﻿// <auto-generated />
using System;
using AuditoriaAPI.Infrasctructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AuditoriaAPI.Migrations
{
    [DbContext(typeof(AuditoriaDbContext))]
    [Migration("20211021121716_RetiraNullableDeValorAntigoValorNovo")]
    partial class RetiraNullableDeValorAntigoValorNovo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Auditoria")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("AuditoriaAPI.Domain.Auditoria", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("DataAlteracao");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("DataCriacao");

                    b.Property<Guid?>("IdEntidade")
                        .HasColumnType("uuid");

                    b.Property<string>("NomeEntidade")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("NomeTabela")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("TipoAuditoria")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Auditoria");
                });

            modelBuilder.Entity("AuditoriaAPI.Domain.AuditoriaPropriedade", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("Id");

                    b.Property<Guid>("AuditoriaId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("DataAlteracao");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("DataCriacao");

                    b.Property<bool>("EhChavePrimaria")
                        .HasColumnType("boolean");

                    b.Property<string>("NomeDaColuna")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("NomeDaPropriedade")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<string>("ValorAntigo")
                        .HasColumnType("text");

                    b.Property<string>("ValorNovo")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuditoriaId");

                    b.ToTable("AuditoriaPropriedade");
                });

            modelBuilder.Entity("AuditoriaAPI.Domain.AuditoriaPropriedade", b =>
                {
                    b.HasOne("AuditoriaAPI.Domain.Auditoria", "Auditoria")
                        .WithMany("Propriedades")
                        .HasForeignKey("AuditoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Auditoria");
                });

            modelBuilder.Entity("AuditoriaAPI.Domain.Auditoria", b =>
                {
                    b.Navigation("Propriedades");
                });
#pragma warning restore 612, 618
        }
    }
}
