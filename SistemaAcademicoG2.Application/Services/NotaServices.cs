using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;

namespace SistemaAcademicoG2.Application.Services
{
    // Algoritmos con lógica de negocio (UseCase)
    public class NotaService
    {
        private readonly INotaRepository _repository;

        public NotaService(INotaRepository repository)
        {
            _repository = repository;
        }

        // Caso de uso: Buscar una nota por su Id (solo activas)
        public async Task<Nota?> ObtenerNotaPorIdAsync(int id)
        {
            if (id <= 0)
                return null; // Id no válido

            var nota = await _repository.GetNotaByIdAsync(id);

            if (nota != null && nota.Estado)
                return nota;

            return null; // No encontrada o inactiva
        }

        // Caso de uso: Modificar una nota
        public async Task<string> ModificarNotaAsync(Nota nota)
        {
            if (nota.Id <= 0)
                return "Error: Id no válido";

            var existente = await _repository.GetNotaByIdAsync(nota.Id);
            if (existente == null)
                return "Error: Nota no encontrada";

            // Actualizamos campos
            existente.IdUsuario = nota.IdUsuario;
            existente.IdAsignatura = nota.IdAsignatura;
            existente.Periodo = nota.Periodo;
            existente.NotaFinal = nota.NotaFinal;
            existente.Estado = nota.Estado;

            // Validación automática de estado académico
            existente.EstadoAcademico = nota.NotaFinal >= 7 ? "Aprobado" : "Reprobado";

            await _repository.UpdateNotaAsync(existente);

            return "Nota modificada correctamente";
        }

        // Caso de uso: Obtener solo notas activas
        public async Task<IEnumerable<Nota>> ObtenerNotasActivasAsync()
        {
            var notas = await _repository.GetNotasAsync();
            return notas.Where(n => n.Estado);
        }

        // Caso de uso: Agregar nota
        public async Task<string> AgregarNotaAsync(Nota nuevaNota)
        {
            try
            {
                var notas = await _repository.GetNotasAsync();

                // Evitar duplicados: mismo usuario, asignatura y periodo
                if (notas.Any(n =>
                    n.IdUsuario == nuevaNota.IdUsuario &&
                    n.IdAsignatura == nuevaNota.IdAsignatura &&
                    n.Periodo.Trim().ToLower() == nuevaNota.Periodo.Trim().ToLower()))
                {
                    return "Error: Ya existe una nota para este usuario, asignatura y periodo";
                }

                nuevaNota.Estado = true; // Activa por defecto

                // Validación automática de estado académico
                nuevaNota.EstadoAcademico = nuevaNota.NotaFinal >= 7 ? "Aprobado" : "Reprobado";

                var notaInsertada = await _repository.AddNotaAsync(nuevaNota);

                if (notaInsertada == null || notaInsertada.Id <= 0)
                    return "Error: no se pudo agregar la nota";

                return "Nota agregada correctamente";
            }
            catch (Exception ex)
            {
                return $"Error de servidor: {ex.Message}";
            }
        }
    }
}


