using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAcademicoG2.Domain.Entities
{
    [Table("T_Asistencias")]
    public class Asistencia
    {
        [Key]
        [Column("IdAsistencia")]
        public int IdAsistencia { get; set; }

        [Required]
        [Column("IdDGA")]
        public int IdDGA { get; set; }

        [Required]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("Fecha")]
        public DateTime Fecha { get; set; }

        [Required, StringLength(15)]
        [Column("EstadoAsistencia")]
        public string EstadoAsistencia { get; set; }

        [StringLength(250)]
        [Column("Observacion")]
        public string Observacion { get; set; }

        [Required]
        [Column("Estado")]
        public bool Estado { get; set; }

        // Relaciones
        [ForeignKey(nameof(IdUsuario))]
        public Usuario? Usuario { get; set; }

        [ForeignKey(nameof(IdDGA))]
        public DocenteAsignaturaGrado? DocenteAsignaturaGrado { get; set; }
    }
}
