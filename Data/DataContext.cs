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
        public DbSet<Voluntario> Voluntarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Voluntario>().ToTable("Voluntarios"); 
            modelBuilder.Entity<ONG>().ToTable("ONGs"); 
        }
        
    }

     
}
