using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;

namespace SistemaAcademicoG2.BL.Services
{
    public class GradoInscripcionService
    {
        private readonly IGradoInscripcionRepository _repository;

        public GradoInscripcionService(IGradoInscripcionRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GradoInscripcion>> GetAllAsync() =>
            await _repository.GetAllAsync();

        public async Task<GradoInscripcion?> GetByIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task<IEnumerable<GradoInscripcion>> GetByGradoIdAsync(int idGrado) =>
            await _repository.GetByGradoIdAsync(idGrado);

        public async Task<IEnumerable<GradoInscripcion>> GetByInscripcionIdAsync(int idInscripcion) =>
            await _repository.GetByInscripcionIdAsync(idInscripcion);

        public async Task<IEnumerable<GradoInscripcion>> GetByEstadoAsync(int estado) =>
            await _repository.GetByEstadoAsync(estado);

        public async Task AddAsync(GradoInscripcion entity) =>
            await _repository.AddAsync(entity);

        public async Task UpdateAsync(GradoInscripcion entity) =>
            await _repository.UpdateAsync(entity);

        public async Task DeleteAsync(int id) =>
            await _repository.DeleteAsync(id);
    }
}
