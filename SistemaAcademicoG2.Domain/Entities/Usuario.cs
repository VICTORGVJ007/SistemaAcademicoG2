using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Domain.Entities
{
    [Table("T_Usuario")]  // Nombre de la tabla en la BD
    public class Usuario
    {
        [Key]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Column("IdRol")]
        public int IdRol { get; set; }

        [Required]
        [Column("Nombre")]
        [MaxLength(100)] 
        public string Nombre { get; set; }

        [Required]
        [Column("Apellido")]
        [MaxLength(100)]
        public string Apellido { get; set; }

        [Required]
        [Column("Correo")]
        [MaxLength(150)]
        public string Correo { get; set; } 

        [Required]
        [Column("Clave")]
        [MaxLength(250)] 
        public string Clave { get; set; }

        [Column("Estado")]
        public bool Estado { get; set; }   


    }
}
