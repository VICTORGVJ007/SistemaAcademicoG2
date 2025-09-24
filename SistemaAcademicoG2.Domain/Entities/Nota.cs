using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Domain.Entities
{
    [Table("t_Nota")]
    public class Nota
    {
        [Key]
        [Column("idnota")]
        public int Id { get; set; }

        [Column("idusuario")]
        [Required]
        public int IdUsuario { get; set; }

        [Column("idasignatura")]
        [Required]
        public int IdAsignatura { get; set; }

        [Column("periodo")]
        [Required, StringLength(20)]
        public string Periodo { get; set; }

        [Column("nota")]
        [Required]
        public decimal NotaFinal { get; set; }

        [Column("estadoacademico")]
        [Required, StringLength(30)]
        public string EstadoAcademico { get; set; }

        [Column("estado")]
        [Required]
        public bool Estado { get; set; }
    }
}

