namespace SistemaAcademicoG2.DTOs
{
    public class InscripcionDTO
    {
        public int IdInscripcion { get; set; }

        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;

        public int IdGrado { get; set; }
        public string NombreGrado { get; set; } = string.Empty;

        public DateTime FechaIngreso { get; set; }
        public int AnioLectivo { get; set; }
        public bool Estado { get; set; }
    }
}
