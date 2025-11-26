using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAcademicoG2.Domain.Entities
{
    [Table("T_DocenteAsignaturaGrado")]
    public class DocenteAsignaturaGrado
    {
        [Key]
        [Column("IdDGA")]
        public int IdDGA { get; set; }

        [Required]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Required]
        [Column("IdGradoAsignatura")]
        public int IdGradoAsignatura { get; set; }

        [Column("Estado")]
        public bool Estado { get; set; }

        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

        [ForeignKey("IdGradoAsignatura")]
        public GradoAsignatura? GradoAsignatura { get; set; }

        public ICollection<Asistencia>? Asistencias { get; set; }

    }
}
