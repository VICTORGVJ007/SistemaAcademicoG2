using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAcademicoG2.Domain.Entities
{
    [Table("Asistencias")]
    public class Asistencia
    {
        [Key]
        [Column("AsistenciaId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }
        [Column("Grado")]
        [Required(ErrorMessage = "El grado es obligatorio")]
        [StringLength(50)]
        public string Grado { get; set; }
        [Column("Fecha")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        [Column("Estado")]
        [Required(ErrorMessage = "El estado es obligatorio")]
        [StringLength(20)]
        public string Estado { get; set; }
    }
}
