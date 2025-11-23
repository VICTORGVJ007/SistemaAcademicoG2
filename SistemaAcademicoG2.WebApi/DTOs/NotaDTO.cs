namespace SistemaAcademicoG2.WebApi.DTOs
{
    public class NotaDTO
    {
        public int IdNota { get; set; }
        public int IdUsuario { get; set; }
        public int IdAsignatura { get; set; }
        public int IdPeriodo { get; set; }

        public decimal Nota1 { get; set; }
        public decimal Nota2 { get; set; }
        public decimal Nota3 { get; set; }
        public decimal PromedioFinal { get; set; }
        public string EstadoAcademico { get; set; } = string.Empty;
        public bool Estado { get; set; }

        // nombres planos para el cliente
        public string NombreUsuario { get; set; } = string.Empty;
        public string NombreAsignatura { get; set; } = string.Empty;
        public string NombrePeriodo { get; set; } = string.Empty;
    }
}
