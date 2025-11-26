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

        // -----------------------------
        //        MÉTODOS GET
        // -----------------------------
        public Task<IEnumerable<DocenteAsignaturaGrado>> ObtenerActivosAsync() =>
            _repository.GetActivosAsync();

        public Task<IEnumerable<DocenteAsignaturaGrado>> ObtenerInactivosAsync() =>
            _repository.GetInactivosAsync();

        public Task<DocenteAsignaturaGrado?> ObtenerPorIdAsync(int id) =>
            _repository.GetByIdAsync(id);

        public Task<List<GradoAsignatura>> ObtenerAsignaturasPorGradoAsync(int idGrado) =>
            _repository.GetAsignaturasPorGradoAsync(idGrado);

        // -----------------------------
        //        MÉTODO AGREGAR
        // -----------------------------
        public async Task<string> AgregarAsync(DocenteAsignaturaGrado entidad)
        {
            if (await _repository.ExisteDuplicadoAsync(entidad.IdUsuario, entidad.IdGradoAsignatura))
                return "Error: Esta asignación ya existe.";

            entidad.Estado = true;

            await _repository.AddAsync(entidad);
            return "Asignación agregada correctamente.";
        }

        // -----------------------------
        //        MÉTODO ACTUALIZAR
        // -----------------------------
        public async Task<string> ActualizarAsync(DocenteAsignaturaGrado entidad)
        {
            var existente = await _repository.GetByIdAsync(entidad.IdDGA);

            if (existente == null)
                return "Error: Registro no encontrado.";

            existente.IdUsuario = entidad.IdUsuario;
            existente.IdGradoAsignatura = entidad.IdGradoAsignatura;
            existente.Estado = entidad.Estado;

            await _repository.SaveChangesAsync();
            return "Asignación actualizada correctamente.";
        }

        // -----------------------------
        //   MÉTODOS ACTIVAR/DESACTIVAR
        // -----------------------------
        public async Task<string> DesactivarAsync(int id)
        {
            if (!await _repository.DesactivarAsync(id))
                return "Error: Registro no encontrado.";

            return "Asignación desactivada correctamente.";
        }

        public async Task<string> ActivarAsync(int id)
        {
            if (!await _repository.ActivarAsync(id))
                return "Error: Registro no encontrado.";

            return "Asignación activada correctamente.";
        }
    }
}



