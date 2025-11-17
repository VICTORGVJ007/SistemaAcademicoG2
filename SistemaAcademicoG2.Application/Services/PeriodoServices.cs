using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;

namespace SistemaAcademicoG2.Application.Services
{
    public class PeriodoServices
    {
        private readonly IPeriodoRepository _repository;

        public PeriodoServices(IPeriodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Periodo>> ObtenerActivosAsync() =>
            await _repository.GetActivosAsync();

        public async Task<IEnumerable<Periodo>> ObtenerInactivosAsync() =>
            await _repository.GetInactivosAsync();

        public async Task<Periodo> ObtenerPorIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task<string> AgregarAsync(Periodo periodo)
        {
            periodo.Estado = true;
            await _repository.AddAsync(periodo);
            return "Periodo agregado correctamente";
        }

        public async Task<string> ModificarAsync(Periodo periodo)
        {
            await _repository.UpdateAsync(periodo);
            return "Periodo actualizado correctamente";
        }

        public async Task<string> DesactivarAsync(int id)
        {
            var ok = await _repository.DesactivarAsync(id);
            return ok ? "Periodo desactivado" : "Error: periodo no encontrado";
        }

        public async Task<string> ActivarAsync(int id)
        {
            var ok = await _repository.ActivarAsync(id);
            return ok ? "Periodo activado" : "Error: periodo no encontrado";
        }
    }
}

