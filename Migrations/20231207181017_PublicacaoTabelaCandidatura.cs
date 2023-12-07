using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AcaoSolidariaApi.Migrations
{
    /// <inheritdoc />
    public partial class PublicacaoTabelaCandidatura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjetoAssociado",
                table: "Publicacoes",
                newName: "VagasDisponiveis");

            migrationBuilder.RenameColumn(
                name: "Conteudo",
                table: "Publicacoes",
                newName: "Titulo");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "Publicacoes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicio",
                table: "Publicacoes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Publicacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Local",
                table: "Publicacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "Publicacoes");

            migrationBuilder.DropColumn(
                name: "DataInicio",
                table: "Publicacoes");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Publicacoes");

            migrationBuilder.DropColumn(
                name: "Local",
                table: "Publicacoes");

            migrationBuilder.RenameColumn(
                name: "VagasDisponiveis",
                table: "Publicacoes",
                newName: "ProjetoAssociado");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "Publicacoes",
                newName: "Conteudo");
        }
    }
}
