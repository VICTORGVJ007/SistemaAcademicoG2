


using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Application.Services
{
    public class GradoAsignaturaService
    {
        private readonly IGradoAsignaturaRepository _repository;

        public GradoAsignaturaService(IGradoAsignaturaRepository repository)
        {
            _repository = repository;
        }

        // Obtener todos
        public async Task<IEnumerable<GradoAsignatura>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Obtener por Id
        public async Task<GradoAsignatura> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        // Agregar
        public async Task AddAsync(GradoAsignatura entity)
        {
            await _repository.AddAsync(entity);
        }

        // Actualizar
        public async Task UpdateAsync(GradoAsignatura entity)
        {
            await _repository.UpdateAsync(entity);
        }

        // Eliminar
        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
