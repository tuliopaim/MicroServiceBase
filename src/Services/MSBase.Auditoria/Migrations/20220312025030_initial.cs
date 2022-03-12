using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSBase.Auditoria.API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AuditoriaEntidade");

            migrationBuilder.CreateTable(
                name: "AuditoriaEntidade",
                schema: "AuditoriaEntidade",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TipoAuditoria = table.Column<string>(type: "varchar(50)", nullable: false),
                    IdEntidade = table.Column<Guid>(type: "uuid", nullable: true),
                    NomeEntidade = table.Column<string>(type: "varchar(200)", nullable: false),
                    NomeTabela = table.Column<string>(type: "varchar(200)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditoriaEntidade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditoriaPropriedade",
                schema: "AuditoriaEntidade",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeDaPropriedade = table.Column<string>(type: "varchar(200)", nullable: false),
                    NomeDaColuna = table.Column<string>(type: "varchar(200)", nullable: false),
                    ValorAntigo = table.Column<string>(type: "text", nullable: true),
                    ValorNovo = table.Column<string>(type: "text", nullable: true),
                    EhChavePrimaria = table.Column<bool>(type: "boolean", nullable: false),
                    AuditoriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditoriaPropriedade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditoriaPropriedade_AuditoriaEntidade_AuditoriaId",
                        column: x => x.AuditoriaId,
                        principalSchema: "AuditoriaEntidade",
                        principalTable: "AuditoriaEntidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditoriaPropriedade_AuditoriaId",
                schema: "AuditoriaEntidade",
                table: "AuditoriaPropriedade",
                column: "AuditoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditoriaPropriedade",
                schema: "AuditoriaEntidade");

            migrationBuilder.DropTable(
                name: "AuditoriaEntidade",
                schema: "AuditoriaEntidade");
        }
    }
}
