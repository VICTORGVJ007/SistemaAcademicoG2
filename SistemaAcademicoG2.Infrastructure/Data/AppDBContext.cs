using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoG2.Domain.Entities;
namespace SistemaAcademicoG2.Infrastructure.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<Rol> Roles { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("t_Usuario");
            modelBuilder.Entity<Asistencia>().ToTable("t_Asistencia");
            modelBuilder.Entity<Rol>().ToTable("t_Rol");

            base.OnModelCreating(modelBuilder);
        }
    }

    
}
