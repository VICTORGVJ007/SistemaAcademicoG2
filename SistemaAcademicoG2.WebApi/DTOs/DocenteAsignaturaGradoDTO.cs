namespace Proyecto.API.DTOs
{
    public class DocenteAsignaturaGradoDTO
    {
        public int IdDGA { get; set; }

        public int IdUsuario { get; set; }
        public string NombreDocente { get; set; } = null!;

        public int IdGradoAsignatura { get; set; }
        public string NombreGrado { get; set; } = null!;
        public string NombreAsignatura { get; set; } = null!;

        public bool Estado { get; set; }
    }

    public class DocenteAsignaturaGradoCrearDTO
    {
        public int IdUsuario { get; set; }
        public int IdGradoAsignatura { get; set; }
    }
}
