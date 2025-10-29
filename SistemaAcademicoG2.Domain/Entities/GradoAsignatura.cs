using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAcademicoG2.Domain.Entities
{
    [Table("t_Grado_asignatura")]
    public class GradoAsignatura
    {
        [Key]
        [Column("IdGradoAsignatura")]
        public int IdGradoAsignatura { get; set; }

        // FK hacia Grado
        [Column("IdGrado")]
        public int IdGrado { get; set; }
        public Grado Grado { get; set; }   // 🔹 Propiedad de navegación

        // FK hacia Asignatura
        [Column("IdAsignatura")]
        public int IdAsignatura { get; set; }
        public Asignatura Asignatura { get; set; }  // 🔹 Propiedad de navegación

        [Column("Estado")]
        public int Estado { get; set; }

    }
}
