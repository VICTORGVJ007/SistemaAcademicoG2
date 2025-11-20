using SistemaAcademicoG2.DTOs;

namespace SistemaAcademicoG2.WebApi.DTOs
{
    public class NotaDTO
    {
        public int IdNota { get; set; }

        public int IdUsuario { get; set; }
        public UsuarioDTO Usuario { get; set; }

        public int IdAsignatura { get; set; }
        public AsignaturaDTO Asignatura { get; set; }

        public int IdPeriodo { get; set; }
        public PeriodoDTO Periodo { get; set; }

        public decimal Nota1 { get; set; }
        public decimal Nota2 { get; set; }
        public decimal Nota3 { get; set; }

        public decimal PromedioFinal { get; set; }
        public string EstadoAcademico { get; set; }

        public bool Estado { get; set; }
    }
}
