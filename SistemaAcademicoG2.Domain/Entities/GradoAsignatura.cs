using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaAcademicoG2.Domain.Entities
{
    [Table("T_GradoAsignatura")]
    public class GradoAsignatura
    {
        [Key]
        [Column("Id")]
        public int IdGradoAsignatura { get; set; }

        [Required(ErrorMessage = "El grado es obligatorio")]
        [Column("GradoId")]
        public int IdGrado { get; set; }

        [Required(ErrorMessage = "La asignatura es obligatoria")]
        [Column("AsignaturaId")]
        public int IdAsignatura { get; set; }

        [Column("Estado")]
        public bool Estado { get; set; }

        // Relaciones
        [ForeignKey(nameof(IdGrado))]
        public Grado Grado { get; set; }

        [ForeignKey(nameof(IdAsignatura))]
        public Asignatura Asignatura { get; set; }

        // Relación con DocenteAsignaturaGrado
        public ICollection<DocenteAsignaturaGrado> DocentesGradoAsignatura { get; set; }
    }
}
