using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AcaoSolidariaApi.Models;


namespace AcaoSolidariaApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<ONG> ONGs { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Publicacao> Publicacoes { get; set; }

        public DbSet<Candidatura> Candidaturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ONG>().ToTable("ONGs");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
            });
            modelBuilder.Entity<Publicacao>().ToTable("Publicacoes");
            modelBuilder.Entity<Candidatura>().ToTable("Candidaturas");
        }



    }


}
