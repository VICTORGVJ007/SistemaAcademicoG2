namespace Proyecto.API.DTOs
{
        public class GradoAsignaturaDTO
        {
            public int IdGradoAsignatura { get; set; }

            // IDs para formularios de selección
            public int IdGrado { get; set; }
            public int IdAsignatura { get; set; }

            // Nombres para mostrar en tabla
            public string NombreGrado { get; set; } = string.Empty;
            public string NombreAsignatura { get; set; } = string.Empty;

            public bool Estado { get; set; }
        }
}
