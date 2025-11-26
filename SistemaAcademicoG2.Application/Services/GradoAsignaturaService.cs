using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GradoAsignaturaService
{
    private readonly IGradoAsignaturaRepository _repository;

    public GradoAsignaturaService(IGradoAsignaturaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GradoAsignatura>> ObtenerActivasAsync() =>
        await _repository.GetActivasAsync();

    public async Task<IEnumerable<GradoAsignatura>> ObtenerInactivasAsync() =>
        await _repository.GetInactivasAsync();

    public async Task<GradoAsignatura?> ObtenerPorIdAsync(int id) =>
        await _repository.GetByIdAsync(id);

    public async Task<string> AgregarGradoAsignaturaAsync(GradoAsignatura entidad)
    {
        entidad.Estado = true;
        await _repository.AddAsync(entidad);
        return "Grado-Asignatura registrada correctamente";
    }

    public async Task<string> AgregarMultipleAsync(GradoAsignaturaMultipleDTO dto)
    {
        foreach (var idAsignatura in dto.IdAsignaturas)
        {
            var entity = new GradoAsignatura
            {
                IdGrado = dto.IdGrado,
                IdAsignatura = idAsignatura,
                Estado = dto.Estado
            };
            await _repository.AddAsync(entity); // Usamos el repository, no _context
        }

        return "Asignaciones creadas correctamente";
    }

    public async Task<string> ModificarAsync(GradoAsignatura entidad)
    {
        await _repository.UpdateAsync(entidad);
        return "Grado-Asignatura actualizada correctamente";
    }

    public async Task<string> DesactivarAsync(int id)
    {
        var ok = await _repository.DesactivarAsync(id);
        return ok ? "Grado-Asignatura desactivada" : "Error: no encontrada";
    }

    public async Task<string> ActivarAsync(int id)
    {
        var ok = await _repository.ActivarAsync(id);
        return ok ? "Grado-Asignatura activada" : "Error: no encontrada";
    }
}
