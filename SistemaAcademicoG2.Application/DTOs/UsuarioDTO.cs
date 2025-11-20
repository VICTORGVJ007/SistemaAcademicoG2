namespace SistemaAcademicoG2.DTOs
{
    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }

        // Relación con Rol
        public int IdRol { get; set; }
        public string? NombreRol { get; set; }  // opcional según tus joins

        // Información personal
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }

        // Estado
        public bool Estado { get; set; }
    }
}
