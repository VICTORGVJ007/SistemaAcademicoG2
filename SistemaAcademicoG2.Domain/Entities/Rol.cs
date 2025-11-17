using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAcademicoG2.Domain.Entities
{
    [Table("Roles")]
    public class Rol
    {
        [Key]
        [Column("IdRol")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRol { get; set; }

        [Required, StringLength(50)]
        [Column("Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Column("Estado")]
        public bool Estado { get; set; }

        // Relación inversa
        public ICollection<Usuario>? Usuarios { get; set; }

    }
}
