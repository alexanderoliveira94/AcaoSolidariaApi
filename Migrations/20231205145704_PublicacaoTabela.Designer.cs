﻿// <auto-generated />
using System;
using AcaoSolidariaApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AcaoSolidariaApi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231205145704_PublicacaoTabela")]
    partial class PublicacaoTabela
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AcaoSolidariaApi.Models.Candidatura", b =>
                {
                    b.Property<int>("IdCandidatura")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCandidatura"));

                    b.Property<DateTime>("DataCandidatura")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdPublicacao")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<int>("PublicacaoIdPublicacao")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioIdUsuario")
                        .HasColumnType("int");

                    b.HasKey("IdCandidatura");

                    b.HasIndex("PublicacaoIdPublicacao");

                    b.HasIndex("UsuarioIdUsuario");

                    b.ToTable("Candidaturas", (string)null);
                });

            modelBuilder.Entity("AcaoSolidariaApi.Models.ONG", b =>
                {
                    b.Property<int?>("IdOng")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("IdOng"));

                    b.Property<string>("CNPJOng")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("DescricaoOng")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailOng")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnderecoOng")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IdFotoOng")
                        .HasColumnType("int");

                    b.Property<string>("NomeOng")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("IdOng");

                    b.ToTable("ONGs", (string)null);
                });

            modelBuilder.Entity("AcaoSolidariaApi.Models.Publicacao", b =>
                {
                    b.Property<int>("IdPublicacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPublicacao"));

                    b.Property<string>("Conteudo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataPublicacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("OngAssociada")
                        .HasColumnType("int");

                    b.Property<int>("ProjetoAssociado")
                        .HasColumnType("int");

                    b.HasKey("IdPublicacao");

                    b.ToTable("Publicacoes", (string)null);
                });

            modelBuilder.Entity("AcaoSolidariaApi.Models.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"));

                    b.Property<float?>("AvaliacaoMedia")
                        .HasColumnType("real");

                    b.Property<DateTime?>("DataRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("DescricaoHabilidades")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("IdFotoUsuario")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("IdUsuario");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Usuarios", (string)null);
                });

            modelBuilder.Entity("AcaoSolidariaApi.Models.Candidatura", b =>
                {
                    b.HasOne("AcaoSolidariaApi.Models.Publicacao", "Publicacao")
                        .WithMany()
                        .HasForeignKey("PublicacaoIdPublicacao")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcaoSolidariaApi.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioIdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publicacao");

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}