namespace  Proyecto.API.DTOs
{
    public class NotaDTO
    {
        // ============================
        // 📌 DATOS DE LA NOTA
        // ============================
        public int IdNota { get; set; }
        public decimal Calificacion { get; set; }
        public bool Estado { get; set; }

        // ============================
        // 📌 FK Y RELACIONES
        // ============================
        public int IdUsuario { get; set; }
        public int IdAsignatura { get; set; }
        public int IdPeriodo { get; set; }

        // ============================
        // 📌 DATOS DEL USUARIO
        // ============================
        public UsuarioDTO? Usuario { get; set; }

        // ============================
        // 📌 DATOS DE LA ASIGNATURA
        // ============================
        public AsignaturaDTO? Asignatura { get; set; }

        // ============================
        // 📌 DATOS DEL PERIODO
        // ============================
        public PeriodoDTO? Periodo { get; set; }
    }

    // =============================================================
    //        SUBDTOs 📌 (viven dentro del mismo archivo)
    // =============================================================

    public class UsuarioDTO
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
    }

    public class AsignaturaDTO
    {
        public int IdAsignatura { get; set; }
        public string Nombre { get; set; } = "";
    }

    public class PeriodoDTO
    {
        public int IdPeriodo { get; set; }
        public string Nombre { get; set; } = "";
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
    }
}
