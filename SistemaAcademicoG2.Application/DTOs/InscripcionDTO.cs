namespace SistemaAcademicoG2.WebApi.DTOs
{
    public class InscripcionDTO
    {
        public int IdInscripcion { get; set; }
        public DateTime FechaIngreso { get; set; }
        public bool Estado { get; set; }

        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }

        public int IdGrado { get; set; }
        public string NombreGrado { get; set; }
    }
}
