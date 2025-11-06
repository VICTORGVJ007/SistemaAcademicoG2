using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaAcademicoG2.Domain.Entities
{
    [Table("T_Usuario")]
    public class Usuario
    {
        // ===========================
        // ✅ ID del usuario
        // ===========================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        // ===========================
        // ✅ Relación con Rol
        // ===========================
        [Required]
        [Column("IdRol")]
        public int IdRol { get; set; }

        [ForeignKey("IdRol")]
        public Rol? Rol { get; set; }

        // ===========================
        // ✅ Información personal
        // ===========================
        [Required]
        [Column("Nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [Column("Apellido")]
        [MaxLength(100)]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress]
        [Column("Correo")]
        [MaxLength(150)]
        public string Correo { get; set; }

        // ===========================
        // ✅ Contraseña Hasheada (se guarda en BD)
        // ===========================
        [Required]
        [Column("Clave")]
        [MaxLength(250)]
        public string PasswordHash { get; set; }

        // ===========================
        // ✅ Contraseña en texto plano SOLO para entrada
        // ===========================
        [NotMapped]
        public string? Password { get; set; }

        // ===========================
        // ✅ Estado
        // ===========================
        [Column("Estado")]
        public bool Estado { get; set; }
    }
}
