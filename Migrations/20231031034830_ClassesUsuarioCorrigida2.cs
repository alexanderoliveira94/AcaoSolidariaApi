using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcaoSolidariaApi.Migrations
{
    /// <inheritdoc />
    public partial class ClassesUsuarioCorrigida2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Voluntarios");

            migrationBuilder.RenameColumn(
                name: "IdFoto",
                table: "Usuarios",
                newName: "IdFotoUsuario");

            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "ONGs",
                newName: "NomeOng");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "ONGs",
                newName: "EnderecoOng");

            migrationBuilder.RenameColumn(
                name: "Endereco",
                table: "ONGs",
                newName: "EmailOng");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "ONGs",
                newName: "DescricaoOng");

            migrationBuilder.RenameColumn(
                name: "CNPJ",
                table: "ONGs",
                newName: "CNPJOng");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ONGs",
                newName: "IdOng");

            migrationBuilder.AddColumn<int>(
                name: "IdFotoOng",
                table: "ONGs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "ONGs",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "ONGs",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdFotoOng",
                table: "ONGs");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "ONGs");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "ONGs");

            migrationBuilder.RenameColumn(
                name: "IdFotoUsuario",
                table: "Usuarios",
                newName: "IdFoto");

            migrationBuilder.RenameColumn(
                name: "NomeOng",
                table: "ONGs",
                newName: "Senha");

            migrationBuilder.RenameColumn(
                name: "EnderecoOng",
                table: "ONGs",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "EmailOng",
                table: "ONGs",
                newName: "Endereco");

            migrationBuilder.RenameColumn(
                name: "DescricaoOng",
                table: "ONGs",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "CNPJOng",
                table: "ONGs",
                newName: "CNPJ");

            migrationBuilder.RenameColumn(
                name: "IdOng",
                table: "ONGs",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "Voluntarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voluntarios", x => x.Id);
                });
        }
    }
}
