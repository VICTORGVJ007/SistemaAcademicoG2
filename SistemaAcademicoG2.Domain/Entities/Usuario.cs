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
        [Column("IdRol")]
        [Required]
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
        // ✅ Manejo de contraseñas
        // ===========================

        // ✅ Nuevo campo adoptado: almacena la clave hasheada en BD
        [Column("Clave")]
        [Required]
        [MaxLength(250)]
        public string PasswordHash { get; set; }

        // ❗ Mantenemos compatibilidad con tu entidad original:
        // Clave seguirá funcionando si ya existe en BD.
        // Si prefieres eliminarlo, te lo puedo remover.
        [NotMapped]
        public string? Password { get; set; }

        // ===========================
        // ✅ Estado
        // ===========================
        [Column("Estado")]
        public bool Estado { get; set; }

        // ===========================
        // ✅ Colecciones opcionales
        // ===========================
        // Solo si tu sistema académico las necesita; si NO, las quitamos.
        // public ICollection<Movimientos>? Movimientos { get; set; }
    }
}
