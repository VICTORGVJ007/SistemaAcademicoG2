using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAcademicoG2.Domain.Entities
{
    [Table("T_Asistencias")]
    public class Asistencia
    {
        [Key] 
        [Column("IdAsistencia")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAsistencia { get; set; }

        [Column ("IdUsuario")]
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public int IdUsuario { get; set; }

        [Column("IdGrado")]
        [Required(ErrorMessage = "El grado es obligatorio")]
        public int IdGrado { get; set; }

        [Column("Fecha")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [Column("Estado")]
        [Required(ErrorMessage = "El estado es obligatorio")]
        public bool Estado { get; set; }



       

    }
}
