using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;
using SistemaAcademicoG2.DTOs;
using SistemaAcademicoG2.WebApi.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Application.Services
{
    public class NotaService
    {
        private readonly INotaRepository _notaRepository;

        public NotaService(INotaRepository notaRepository)
        {
            _notaRepository = notaRepository;
        }

        // =============================
        // LISTAR NOTAS ACTIVAS
        // =============================
        public async Task<IEnumerable<NotaDTO>> ObtenerNotasActivasAsync()
        {
            var notas = await _notaRepository.GetNotasWithIncludesAsync();
            return notas.Where(n => n.Estado).Select(MapToDTO).ToList();
        }

        // =============================
        // LISTAR NOTAS INACTIVAS
        // =============================
        public async Task<IEnumerable<NotaDTO>> ObtenerNotasInactivasAsync()
        {
            var notas = await _notaRepository.GetNotasWithIncludesAsync();
            return notas.Where(n => !n.Estado).Select(MapToDTO).ToList();
        }

        // =============================
        // OBTENER POR ID
        // =============================
        public async Task<NotaDTO?> ObtenerNotaPorIdAsync(int id)
        {
            var nota = await _notaRepository.GetNotaByIdWithIncludesAsync(id);
            return nota == null ? null : MapToDTO(nota);
        }

         ///=============================
        /// AGREGAR NOTA
         /// =============================
         
        public async Task<string> AgregarNotaAsync(Nota nota)
        {
            if (nota == null)
                return "Error: La nota enviada es inválida.";

            nota.PromedioFinal = (nota.Nota1 + nota.Nota2 + nota.Nota3) / 3;
            nota.EstadoAcademico = nota.PromedioFinal >= 6 ? "Aprobado" : "Reprobado";
            nota.Estado = true;

            await _notaRepository.AddNotaAsync(nota);
            return "Nota creada exitosamente.";
        }

        // =============================
        // MODIFICAR NOTA
        // =============================
        public async Task<string> ModificarNotaAsync(Nota nota)
        {
            if (!await _notaRepository.NotaExistsAsync(nota.IdNota))
                return $"Error: No existe una nota con ID {nota.IdNota}.";

            nota.PromedioFinal = (nota.Nota1 + nota.Nota2 + nota.Nota3) / 3;
            nota.EstadoAcademico = nota.PromedioFinal >= 6 ? "Aprobado" : "Reprobado";

            await _notaRepository.UpdateNotaAsync(nota);
            return "Nota actualizada correctamente.";
        }

        // =============================
        // DESACTIVAR NOTA
        // =============================
        public async Task<string> DesactivarNotaAsync(int id)
        {
            if (!await _notaRepository.NotaExistsAsync(id))
                return $"Error: No existe una nota con ID {id}.";

            bool ok = await _notaRepository.DesactivarNotaAsync(id);
            return ok ? "Nota desactivada." : "Error: No se pudo desactivar.";
        }

        // =============================
        // MAPEOS
        // =============================
        private NotaDTO MapToDTO(Nota n)
        {
            return new NotaDTO
            {
                IdNota = n.IdNota,
                IdUsuario = n.IdUsuario,
                IdAsignatura = n.IdAsignatura,
                IdPeriodo = n.IdPeriodo,

                Nota1 = n.Nota1,
                Nota2 = n.Nota2,
                Nota3 = n.Nota3,
                PromedioFinal = n.PromedioFinal,
                EstadoAcademico = n.EstadoAcademico,
                Estado = n.Estado,

                Usuario = n.Usuario == null ? null : new UsuarioDTO
                {
                    IdUsuario = n.Usuario.IdUsuario,
                    Nombre = n.Usuario.Nombre,
                    Apellido = n.Usuario.Apellido
                },

                Asignatura = n.Asignatura == null ? null : new AsignaturaDTO
                {
                    IdAsignatura = n.Asignatura.IdAsignatura,
                    Nombre = n.Asignatura.Nombre
                },

                Periodo = n.Periodo == null ? null : new PeriodoDTO
                {
                    IdPeriodo = n.Periodo.IdPeriodo,
                    Nombre = n.Periodo.Nombre,
                    FechaInicio = n.Periodo.FechaInicio,
                    FechaFin = n.Periodo.FechaFin
                }
            };
        }
    }
}
