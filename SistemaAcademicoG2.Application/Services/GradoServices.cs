using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;

namespace SistemaAcademicoG2.Application.Services
{
    public class GradoService
    {
        private readonly IGradoRepository _repository;

        public GradoService(IGradoRepository repository)
        {
            _repository = repository;
        }

        // Obtener todos los grados
        public async Task<IEnumerable<Grado>> ObtenerTodosAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Obtener un grado por Id
        public async Task<Grado?> ObtenerPorIdAsync(int idGrado)
        {
            return await _repository.GetByIdAsync(idGrado);
        }

        // Agregar un nuevo grado (evitar duplicados por IdGrado)
        public async Task<string> AgregarGradoAsync(Grado nuevoGrado)
        {
            try
            {
                var existe = await _repository.GradoExistsAsync(nuevoGrado.IdGrado);
                if (existe)
                    return "Error: Ya existe un grado con ese Id";

                await _repository.AddAsync(nuevoGrado);
                return "Grado registrado correctamente";
            }
            catch (Exception ex)
            {
                return "Error de servidor: " + ex.Message;
            }
        }

        // Actualizar un grado existente
        public async Task<string> ActualizarGradoAsync(Grado grado)
        {
            try
            {
                var existe = await _repository.GradoExistsAsync(grado.IdGrado);
                if (!existe)
                    return "Error: No se encontró el grado";

                await _repository.UpdateAsync(grado);
                return "Grado actualizado correctamente";
            }
            catch (Exception ex)
            {
                return "Error de servidor: " + ex.Message;
            }
        }

        // Eliminar un grado por Id
        public async Task<string> EliminarGradoAsync(int idGrado)
        {
            try
            {
                var existe = await _repository.GradoExistsAsync(idGrado);
                if (!existe)
                    return "Error: No se encontró el grado";

                await _repository.DeleteAsync(idGrado);
                return "Grado eliminado correctamente";
            }
            catch (Exception ex)
            {
                return "Error de servidor: " + ex.Message;
            }
        }
    }
}
