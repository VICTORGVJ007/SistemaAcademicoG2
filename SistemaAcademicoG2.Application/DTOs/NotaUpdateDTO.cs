namespace SistemaAcademicoG2.WebApi.DTOs
{
    public class NotaUpdateDTO
    {
        public int IdUsuario { get; set; }
        public int IdAsignatura { get; set; }
        public int IdPeriodo { get; set; }
        public decimal Nota1 { get; set; }
        public decimal Nota2 { get; set; }
        public decimal Nota3 { get; set; }
    }
}
