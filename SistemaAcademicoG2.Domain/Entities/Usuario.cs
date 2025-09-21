using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Domain.Entities
{
    [Table("Usuario")]  // Nombre de la tabla en la BD
    public class Usuario
    {
        [Key]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Column("IdRol")]
        public int IdRol { get; set; }

        [Column("Usuario")]
        public string Usuarios { get; set; }  // debería ser string, no int

        [Column("Apellido")]
        public string Apellido { get; set; } // también mejor como string

        [Column("Clave")]
        [MaxLength(50)]
        public string Clave { get; set; }
    }
}
