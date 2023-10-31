using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcaoSolidariaApi.Migrations
{
    /// <inheritdoc />
    public partial class ClassesUsuarioCorrigida : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Habilidades",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Usuarios",
                newName: "DescricaoHabilidades");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Usuarios",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Usuarios",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "DescricaoHabilidades",
                table: "Usuarios",
                newName: "Senha");

            migrationBuilder.AddColumn<string>(
                name: "Habilidades",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
