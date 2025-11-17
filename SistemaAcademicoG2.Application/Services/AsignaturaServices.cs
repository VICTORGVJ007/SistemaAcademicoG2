using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;

namespace SistemaAcademicoG2.Application.Services
{
    public class AsignaturaService
    {
        private readonly IAsignaturaRepository _repository;

        public AsignaturaService(IAsignaturaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Asignatura>> ObtenerActivasAsync()
        {
            return await _repository.GetAsignaturasActivasAsync();
        }

        public async Task<IEnumerable<Asignatura>> ObtenerInactivasAsync()
        {
            return await _repository.GetAsignaturasInactivasAsync();
        }

        public async Task<Asignatura?> ObtenerPorIdAsync(int id)
        {
            if (id <= 0) return null;
            return await _repository.GetAsignaturaByIdAsync(id);
        }

        public async Task<string> AgregarAsync(Asignatura asignatura)
        {
            if (await _repository.NombreExisteAsync(asignatura.Nombre))
                return "Error: Ya existe una asignatura con ese nombre.";

            asignatura.Estado = true;
            await _repository.AddAsignaturaAsync(asignatura);
            return "Asignatura agregada correctamente.";
        }

        public async Task<string> ModificarAsync(Asignatura asignatura)
        {
            var existente = await _repository.GetAsignaturaByIdAsync(asignatura.IdAsignatura);
            if (existente == null) return "Error: Asignatura no encontrada.";

            existente.Nombre = asignatura.Nombre;
            existente.Estado = asignatura.Estado;

            await _repository.UpdateAsignaturaAsync(existente);
            return "Asignatura modificada correctamente.";
        }

        public async Task<string> DesactivarAsync(int id)
        {
            if (!await _repository.AsignaturaExistsAsync(id))
                return "Error: Asignatura no encontrada.";

            await _repository.DesactivarAsignaturaAsync(id);
            return "Asignatura desactivada correctamente.";
        }

        public async Task<string> ActivarAsync(int id)
        {
            if (!await _repository.AsignaturaExistsAsync(id))
                return "Error: Asignatura no encontrada.";

            await _repository.ActivarAsignaturaAsync(id);
            return "Asignatura activada correctamente.";
        }
    }
}