namespace SistemaAcademicoG2.Application.DTOs
{
    public class NotaMultipleDTO
    {
        // Nota para un mismo estudiante
        public int IdUsuario { get; set; }

        // Mismo período para todas las notas
        public int IdPeriodo { get; set; }

        // Lista de notas por asignatura
        public List<NotaItemDTO> Notas { get; set; } = new();
    }

    // Sub-DTO que representa cada nota por asignatura
    public class NotaItemDTO
    {
        public int IdAsignatura { get; set; }

        public decimal Nota1 { get; set; }
        public decimal Nota2 { get; set; }
        public decimal Nota3 { get; set; }
    }
}
