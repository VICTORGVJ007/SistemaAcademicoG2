using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Domain.Entities
{
    [Table("t_Periodo")]
    public class Periodo
    {
        [Key]
        [Column("IdPeriodo")]
        public int IdPeriodo { get; set; }

        [Column("Nombre")]
        [Required(ErrorMessage = "El nombre del periodo es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Column("FechaInicio")]
        [Required(ErrorMessage = "La fecha es obligatoria")]
        public DateTime FechaInicio { get; set; }

        [Column("FechaFin")]
        [Required(ErrorMessage = "La fecha es obligatoria")]   
        public DateTime FechaFin { get; set; }

        [Column("Estado")]
        [Required]
        public bool Estado { get; set; }

        public ICollection<Nota> Notas { get; set; }
    }
}
