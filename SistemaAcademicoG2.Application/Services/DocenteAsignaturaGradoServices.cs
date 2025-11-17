using SistemaAcademicoG2.DAL.Repositories;
using SistemaAcademicoG2.Domain.Entities;

public class DocenteAsignaturaGradoService
{
    private readonly IDocenteAsignaturaGradoRepository _repo;

    public DocenteAsignaturaGradoService(IDocenteAsignaturaGradoRepository repo)
    {
        _repo = repo;
    }

    public Task<IEnumerable<DocenteAsignaturaGrado>> ObtenerActivosAsync() =>
        _repo.GetActivosAsync();

    public Task<IEnumerable<DocenteAsignaturaGrado>> ObtenerInactivosAsync() =>
        _repo.GetInactivosAsync();

    public Task<DocenteAsignaturaGrado?> ObtenerPorIdAsync(int id) =>
        _repo.GetByIdAsync(id);

    public async Task<string> AgregarAsync(DocenteAsignaturaGrado entidad)
    {
        // Validación: evitar relaciones duplicadas
        if (await _repo.ExisteRelacionAsync(entidad.IdUsuario, entidad.IdGrado, entidad.IdAsignatura))
            return "Error: Ya existe esta relación docente-asignatura-grado.";

        entidad.Estado = true;
        await _repo.AddAsync(entidad);
        return "Asignación creada correctamente.";
    }

    public async Task<string> ModificarAsync(DocenteAsignaturaGrado entidad)
    {
        await _repo.UpdateAsync(entidad);
        return "Registro modificado correctamente.";
    }

    public async Task<string> DesactivarAsync(int id)
    {
        var ok = await _repo.DesactivarAsync(id);
        return ok ? "Registro desactivado." : "Error: no encontrado.";
    }

    public async Task<string> ActivarAsync(int id)
    {
        var ok = await _repo.ActivarAsync(id);
        return ok ? "Registro activado." : "Error: no encontrado.";
    }
}
