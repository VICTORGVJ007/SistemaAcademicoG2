using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Domain.Entities
{
    [Table("T_Estudiante")]
    public class Estudiante
    {
        [Key]
        [Column("IdEstudiante")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEstudiante { get; set; }

        [Column("IdUsuario")]
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public int IdUsuario { get; set; }

        [Column("FechaNacimiento")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Column("Sexo")]
        [Required(ErrorMessage = "El sexo  es obligatorio")]
        public bool Sexo { get; set; }


        [Column("NombreEncargado")]
        [Required(ErrorMessage = "El nombre del grado es obligatorio")]
        [StringLength(100)]
        public string NombreEncargado { get; set; }

        [Column("TelefonoEncargado")]
        [Required(ErrorMessage = "El teléfono del encargado es obligatorio")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "El teléfono debe tener exactamente 8 dígitos")]
        [RegularExpression("^[0-9]{8}$", ErrorMessage = "El teléfono solo debe contener números y tener 8 dígitos")]
        public string TelefonoEncargado { get; set; }

        [Column("Estado")]
        [Required]
        public bool Estado { get; set; }
    }
}
