using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAcademicoG2.Domain.Entities
{
    [Table("Roles")]
    public class Rol
    {
        [Key]
        [Column("RolId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Nombre")]
        [Required(ErrorMessage = "El nombre del rol es obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Column("Estado")]
        [Required(ErrorMessage = "El estado es obligatorio")]
        [StringLength(20)]
        public string Estado { get; set; }
    }
}
