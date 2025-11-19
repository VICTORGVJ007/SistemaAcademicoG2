using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;

namespace SistemaAcademicoG2.Application.Services
{
    public class DocenteAsignaturaGradoService
    {
        private readonly IDocenteAsignaturaGradoRepository _repository;

        public DocenteAsignaturaGradoService(IDocenteAsignaturaGradoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DocenteAsignaturaGrado>> ObtenerActivosAsync()
        {
            return await _repository.GetActivosAsync();
        }

        public async Task<IEnumerable<DocenteAsignaturaGrado>> ObtenerInactivosAsync()
        {
            return await _repository.GetInactivosAsync();
        }

        public async Task<DocenteAsignaturaGrado?> ObtenerPorIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<string> AgregarAsync(DocenteAsignaturaGrado entidad)
        {
            if (await _repository.ExisteDuplicadoAsync(entidad.IdUsuario, entidad.IdGrado, entidad.IdAsignatura))
                return "Error: Esta relación ya existe.";

            entidad.Estado = true;
            await _repository.AddAsync(entidad);
            return "Asignación creada correctamente.";
        }

        public async Task<string> ActualizarAsync(DocenteAsignaturaGrado entidad)
        {
            var existente = await _repository.GetByIdAsync(entidad.IdDGA);
            if (existente == null)
                return "Error: Registro no encontrado.";

            existente.IdUsuario = entidad.IdUsuario;
            existente.IdGrado = entidad.IdGrado;
            existente.IdAsignatura = entidad.IdAsignatura;
            existente.Estado = entidad.Estado;

            await _repository.SaveChangesAsync();
            return "Asignación actualizada correctamente.";
        }

        public async Task<string> DesactivarAsync(int id)
        {
            if (!await _repository.DesactivarAsync(id))
                return "Error: No se encontró el registro.";

            return "Asignación desactivada correctamente.";
        }

        public async Task<string> ActivarAsync(int id)
        {
            if (!await _repository.ActivarAsync(id))
                return "Error: No se encontró el registro.";

            return "Asignación activada correctamente.";
        }
    }
}
