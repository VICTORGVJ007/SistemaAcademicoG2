using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademico.Domain.Entities
{
    [Table("t_Inscripcion")]
    public class Inscripcion
    {
        [Key]
        [Column("idInscripcion")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "El ID de la inscripción es obligatorio")]
        public int IdInscripcion { get; set; }

        [Column("IdUsuario")]
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public int IdUsuario { get; set; }

        [Column("idGrado")]
        [Required(ErrorMessage = "El grado es obligatorio")]
        public int IdGrado { get; set; }

        [Column("fechaIngreso")]
        [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }

        [Column("estado")]
        [Required(ErrorMessage = "El estado de la inscripción es obligatorio")]
        public bool Estado { get; set; }

    }
}