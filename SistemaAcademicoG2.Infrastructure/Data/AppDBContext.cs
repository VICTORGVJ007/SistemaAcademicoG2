using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaAcademico.Domain.Entities;
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
        public DbSet<Grado> Grados { get; set; }
        public DbSet<Inscripcion> Inscripciones { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbSet<GradoInscripcion> GradoInscripciones { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("t_Usuario");
            modelBuilder.Entity<Asistencia>().ToTable("t_Asistencia");
            modelBuilder.Entity<Rol>().ToTable("t_Rol");
            modelBuilder.Entity<Grado>().ToTable("t_Grado");
            modelBuilder.Entity<Inscripcion>().ToTable("t_Inscripcion");
            modelBuilder.Entity<Asignatura>().ToTable("t_Asignatura");
            modelBuilder.Entity<Nota>().ToTable("t_Nota");
            modelBuilder.Entity<GradoInscripcion>().ToTable("t_GradoInscripcion");

            base.OnModelCreating(modelBuilder);
        }
    }

    
}
