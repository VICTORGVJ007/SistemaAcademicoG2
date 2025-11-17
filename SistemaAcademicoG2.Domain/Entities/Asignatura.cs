using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Domain.Entities
{
    [Table("t_Asignatura")]
    public class Asignatura
    {
        [Key]
        [Column("IdAsignatura")]
        public int IdAsignatura { get; set; }

        [Required, StringLength(50)]
        [Column("Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Column("Estado")]
        public bool Estado { get; set; }

        // Relaciones inversas
        public ICollection<Nota>? Notas { get; set; }
        public ICollection<DocenteAsignaturaGrado>? Docentes { get; set; } // Relación con GradoAsignatura
    }
}

