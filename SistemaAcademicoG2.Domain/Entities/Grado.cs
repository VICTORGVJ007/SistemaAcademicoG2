using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Domain.Entities
{
    [Table("t_grado")]
    public class Grado
    {
        [Key]
        [Column("idGrado")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "El ID del grado es obligatorio")]
        public int IdGrado { get; set; }

        [Column("nombre")]
        [Required(ErrorMessage = "El nombre del grado es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Column("estado")]
        [Required(ErrorMessage = "El estado del grado es obligatorio")]
        public bool Estado { get; set; }
    }
}
