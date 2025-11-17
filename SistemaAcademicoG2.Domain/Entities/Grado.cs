using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SistemaAcademicoG2.Domain.Entities
{
    [Table("t_Grado")]
    public class Grado
    {
        [Key]
        [Column("idGrado")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGrado { get; set; }

        [Column("nombre")]
        [Required(ErrorMessage = "El nombre del grado es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Column("estado")]
        [Required(ErrorMessage = "El estado del grado es obligatorio")]
        public bool Estado { get; set; }

        public ICollection<GradoAsignatura>? GradosAsignaturas { get; set; }
    }
}
