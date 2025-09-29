using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaAcademico.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;

namespace SistemaAcademicoG2.Application.Services
{
    public class InscripcionService
    {
        private readonly IInscripcionRepository _repository;

        public InscripcionService(IInscripcionRepository repository)
        {
            _repository = repository;
        }

        // Obtener todas las inscripciones
        public async Task<IEnumerable<Inscripcion>> ObtenerTodasAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Obtener una inscripción por Id
        public async Task<Inscripcion?> ObtenerPorIdAsync(int idInscripcion)
        {
            return await _repository.GetByIdAsync(idInscripcion);
        }

        // Agregar una nueva inscripción (evitar duplicados por Id)
        public async Task<string> AgregarInscripcionAsync(Inscripcion nuevaInscripcion)
        {
            try
            {
                var existe = await _repository.InscripcionExistsAsync(nuevaInscripcion.IdInscripcion);
                if (existe)
                    return "Error: Ya existe una inscripción con ese Id";

                await _repository.AddAsync(nuevaInscripcion);
                return "Inscripción registrada correctamente";
            }
            catch (Exception ex)
            {
                return "Error de servidor: " + ex.Message;
            }
        }

        // Actualizar una inscripción existente
        public async Task<string> ActualizarInscripcionAsync(Inscripcion inscripcion)
        {
            try
            {
                var existe = await _repository.InscripcionExistsAsync(inscripcion.IdInscripcion);
                if (!existe)
                    return "Error: No se encontró la inscripción";

                await _repository.UpdateAsync(inscripcion);
                return "Inscripción actualizada correctamente";
            }
            catch (Exception ex)
            {
                return "Error de servidor: " + ex.Message;
            }
        }

        // Caso de uso adicional: Obtener inscripciones por grado
        public async Task<IEnumerable<Inscripcion>> ObtenerPorGradoAsync(int idGrado)
        {
            return await _repository.GetByGradoIdAsync(idGrado);
        }

        // Caso de uso adicional: Obtener inscripciones por usuario
        public async Task<IEnumerable<Inscripcion>> ObtenerPorUsuarioAsync(int idUsuario)
        {
            return await _repository.GetByUsuarioIdAsync(idUsuario);
        }
    }
}
