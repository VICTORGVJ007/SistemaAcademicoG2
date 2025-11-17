using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAcademicoG2.Domain.Entities
{
    [Table("T_Notas")]
    public class Nota
    {
        [Key]
        [Column("IdNota")]
        public int IdNota { get; set; }

        [Required]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("IdAsignatura")]
        public int IdAsignatura { get; set; }

        [Required]
        [Column("IdPeriodo")]
        public int IdPeriodo { get; set; }

        [Required]
        [Column("Nota1")]
        public decimal Nota1 { get; set; }

        [Required]
        [Column("Nota2")]
        public decimal Nota2 { get; set; }

        [Required]
        [Column("Nota3")]
        public decimal Nota3 { get; set; }

        [Required]
        [Column("PromedioFinal")]
        public decimal PromedioFinal { get; set; }

        [Required, StringLength(30)]
        [Column("EstadoAcademico")]
        public string EstadoAcademico { get; set; }

        [Column("Estado")]
        public bool Estado { get; set; }

        // Relaciones
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }

        [ForeignKey("IdAsignatura")]
        public Asignatura Asignatura { get; set; }

        [ForeignKey("IdPeriodo")]
        public Periodo Periodo { get; set; }
    }
}


