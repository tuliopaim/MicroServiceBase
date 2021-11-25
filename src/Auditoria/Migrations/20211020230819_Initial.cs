using Microsoft.EntityFrameworkCore.Migrations;

namespace AuditoriaAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Auditoria");

            migrationBuilder.CreateTable(
                name: "Auditoria",
                schema: "Auditoria",
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
                    table.PrimaryKey("PK_Auditoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditoriaPropriedade",
                schema: "Auditoria",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AuditoriaId = table.Column<Guid>(type: "uuid", nullable: false),
                    NomeDaPropriedade = table.Column<string>(type: "varchar(200)", nullable: false),
                    NomeDaColuna = table.Column<string>(type: "varchar(200)", nullable: false),
                    ValorAntigo = table.Column<string>(type: "text", nullable: false),
                    ValorNovo = table.Column<string>(type: "text", nullable: false),
                    EhChavePrimaria = table.Column<bool>(type: "boolean", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditoriaPropriedade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuditoriaPropriedade_Auditoria_AuditoriaId",
                        column: x => x.AuditoriaId,
                        principalSchema: "Auditoria",
                        principalTable: "Auditoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditoriaPropriedade_AuditoriaId",
                schema: "Auditoria",
                table: "AuditoriaPropriedade",
                column: "AuditoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditoriaPropriedade",
                schema: "Auditoria");

            migrationBuilder.DropTable(
                name: "Auditoria",
                schema: "Auditoria");
        }
    }
}
