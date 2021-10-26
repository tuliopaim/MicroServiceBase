using Microsoft.EntityFrameworkCore.Migrations;

namespace AuditoriaAPI.Migrations
{
    public partial class RetiraNullableDeValorAntigoValorNovo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ValorNovo",
                schema: "Auditoria",
                table: "AuditoriaPropriedade",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ValorAntigo",
                schema: "Auditoria",
                table: "AuditoriaPropriedade",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ValorNovo",
                schema: "Auditoria",
                table: "AuditoriaPropriedade",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ValorAntigo",
                schema: "Auditoria",
                table: "AuditoriaPropriedade",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
