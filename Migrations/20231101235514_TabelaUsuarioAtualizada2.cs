using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcaoSolidariaApi.Migrations
{
    /// <inheritdoc />
    public partial class TabelaUsuarioAtualizada2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvaliacaoMedia",
                table: "Usuarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "AvaliacaoMedia",
                table: "Usuarios",
                type: "real",
                nullable: true);
        }
    }
}
