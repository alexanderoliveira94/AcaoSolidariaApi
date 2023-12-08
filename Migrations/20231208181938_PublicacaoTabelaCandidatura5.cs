using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcaoSolidariaApi.Migrations
{
    /// <inheritdoc />
    public partial class PublicacaoTabelaCandidatura5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidaturas_Publicacoes_PublicacaoIdPublicacao",
                table: "Candidaturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidaturas_Usuarios_UsuarioIdUsuario",
                table: "Candidaturas");

            migrationBuilder.DropIndex(
                name: "IX_Candidaturas_PublicacaoIdPublicacao",
                table: "Candidaturas");

            migrationBuilder.DropIndex(
                name: "IX_Candidaturas_UsuarioIdUsuario",
                table: "Candidaturas");

            migrationBuilder.DropColumn(
                name: "PublicacaoIdPublicacao",
                table: "Candidaturas");

            migrationBuilder.DropColumn(
                name: "UsuarioIdUsuario",
                table: "Candidaturas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PublicacaoIdPublicacao",
                table: "Candidaturas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioIdUsuario",
                table: "Candidaturas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidaturas_PublicacaoIdPublicacao",
                table: "Candidaturas",
                column: "PublicacaoIdPublicacao");

            migrationBuilder.CreateIndex(
                name: "IX_Candidaturas_UsuarioIdUsuario",
                table: "Candidaturas",
                column: "UsuarioIdUsuario");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidaturas_Publicacoes_PublicacaoIdPublicacao",
                table: "Candidaturas",
                column: "PublicacaoIdPublicacao",
                principalTable: "Publicacoes",
                principalColumn: "IdPublicacao");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidaturas_Usuarios_UsuarioIdUsuario",
                table: "Candidaturas",
                column: "UsuarioIdUsuario",
                principalTable: "Usuarios",
                principalColumn: "IdUsuario");
        }
    }
}
