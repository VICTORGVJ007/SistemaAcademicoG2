using SistemaAcademico.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAcademicoG2.Domain.Entities
{
    [Table("T_Grado_Inscripcion")]
    public class GradoInscripcion
    {
        [Key]
        [Column("IdGradoInscripcion")]
        public int IdGradoInscripcion { get; set; }

        // ===========================
        // ✅ Relación con Grado
        // ===========================
        [Column("IdGrado")]
        [ForeignKey(nameof(Grado))]
        public int IdGrado { get; set; }

        public Grado? Grado { get; set; }  // ✅ navegación opcional (recomendado)

        // ===========================
        // ✅ Relación con Inscripcion
        // ===========================
        [Column("IdInscripcion")]
        [ForeignKey(nameof(Inscripcion))]
        public int IdInscripcion { get; set; }

        public Inscripcion? Inscripcion { get; set; }

        // ===========================
        // ✅ Estado
        // ===========================
        [Column("Estado")]
        public int Estado { get; set; }
    }
}
