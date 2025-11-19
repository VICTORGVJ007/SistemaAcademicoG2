namespace Proyecto.API.DTOs
{
    public class DocenteAsignaturaGradoDTO
    {
        public int IdDGA { get; set; }

        public int IdUsuario { get; set; }
        public string NombreDocente { get; set; } = null!;

        public int IdGrado { get; set; }
        public string NombreGrado { get; set; } = null!;

        public int IdAsignatura { get; set; }
        public string NombreAsignatura { get; set; } = null!;

        public bool Estado { get; set; }
    }
}
