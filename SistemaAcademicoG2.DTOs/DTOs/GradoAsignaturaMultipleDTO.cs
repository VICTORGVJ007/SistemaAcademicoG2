namespace SistemaAcademicoG2.DTOs
{
    public class GradoAsignaturaMultipleDTO
    {
        public int IdGrado { get; set; }
        public List<int> IdAsignaturas { get; set; } = new();
        public bool Estado { get; set; } = true;
    }
}
