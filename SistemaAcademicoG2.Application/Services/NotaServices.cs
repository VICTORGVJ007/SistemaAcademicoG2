using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;

namespace SistemaAcademicoG2.Application.Services
{
    public class NotaService
    {
        private readonly INotaRepository _repository;

        public NotaService(INotaRepository repository)
        {
            _repository = repository;
        }

        // ==============================
        //   OBTENER NOTA POR ID
        // ==============================
        public async Task<Nota?> ObtenerNotaPorIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var nota = await _repository.GetNotaByIdAsync(id);

            return nota != null && nota.Estado ? nota : null;
        }

        // ==============================
        //     AGREGAR NOTA NUEVA
        // ==============================
        public async Task<string> AgregarNotaAsync(Nota nuevaNota)
        {
            try
            {
                var notas = await _repository.GetNotasAsync();

                // Validar duplicados (solo un registro por alumno, asignatura, periodo)
                if (notas.Any(n =>
                    n.IdUsuario == nuevaNota.IdUsuario &&
                    n.IdAsignatura == nuevaNota.IdAsignatura &&
                    n.IdPeriodo == nuevaNota.IdPeriodo))
                {
                    return "Error: Ya existe una nota para este usuario, asignatura y período";
                }

                // Cálculo automático del promedio
                nuevaNota.PromedioFinal = (nuevaNota.Nota1 + nuevaNota.Nota2 + nuevaNota.Nota3) / 3;

                // Estado académico automático
                nuevaNota.EstadoAcademico = nuevaNota.PromedioFinal >= 7 ? "Aprobado" : "Reprobado";

                nuevaNota.Estado = true; // Activa por defecto

                var result = await _repository.AddNotaAsync(nuevaNota);

                if (result == null || result.IdNota <= 0)
                    return "Error: No se pudo agregar la nota";

                return "Nota agregada correctamente";
            }
            catch (Exception ex)
            {
                return $"Error de servidor: {ex.Message}";
            }
        }

        // ==============================
        //     MODIFICAR NOTA EXISTENTE
        // ==============================
        public async Task<string> ModificarNotaAsync(Nota nota)
        {
            if (nota.IdNota <= 0)
                return "Error: Id de nota no válido";

            var existente = await _repository.GetNotaByIdAsync(nota.IdNota);

            if (existente == null)
                return "Error: Nota no encontrada";

            // Actualizar campos
            existente.IdUsuario = nota.IdUsuario;
            existente.IdAsignatura = nota.IdAsignatura;
            existente.IdPeriodo = nota.IdPeriodo;

            existente.Nota1 = nota.Nota1;
            existente.Nota2 = nota.Nota2;
            existente.Nota3 = nota.Nota3;

            // Recalcular promedio automáticamente
            existente.PromedioFinal = (nota.Nota1 + nota.Nota2 + nota.Nota3) / 3;

            // Estado académico automático
            existente.EstadoAcademico = existente.PromedioFinal >= 7 ? "Aprobado" : "Reprobado";

            existente.Estado = nota.Estado;

            await _repository.UpdateNotaAsync(existente);

            return "Nota modificada correctamente";
        }

        //Desactivar nota
        public async Task<string> DesactivarNotaAsync(int id)
        {
            if (!await _repository.NotaExistsAsync(id))
                return "Error: Nota no encontrada";

            var resultado = await _repository.DesactivarNotaAsync(id);

            return resultado ? "Nota desactivada correctamente" : "Error al desactivar la nota";
        }

        // ==============================
        //   LISTAR NOTAS ACTIVAS
        // ==============================
        public async Task<IEnumerable<Nota>> ObtenerNotasActivasAsync()
        {
            var notas = await _repository.GetNotasAsync();
            return notas.Where(n => n.Estado);
        }

        // ==============================
        //     LISTAR NOTAS INACTIVAS
        // ==============================
        public async Task<IEnumerable<Nota>> ObtenerNotasInactivasAsync()
        {
            var notas = await _repository.GetNotasAsync();
            return notas.Where(n => !n.Estado);
        }
    }
}
