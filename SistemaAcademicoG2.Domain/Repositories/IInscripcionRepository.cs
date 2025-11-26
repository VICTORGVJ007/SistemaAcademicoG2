using SistemaAcademico.Domain.Entities;
namespace Proyecto.API.DTOs;
public interface IInscripcionRepository
{
    // CRUD básico
    Task<IEnumerable<Inscripcion>> GetAllAsync();
    Task<Inscripcion> GetByIdAsync(int id);
    Task AddAsync(Inscripcion inscripcion);
    Task UpdateAsync(Inscripcion inscripcion);


    // Nuevo método
    Task CambiarEstadoAsync(int id);

    // Extras útiles
    Task<IEnumerable<Inscripcion>> GetByUsuarioIdAsync(int usuarioId);
    Task<IEnumerable<Inscripcion>> GetByGradoIdAsync(int gradoId);
    Task<IEnumerable<Inscripcion>> GetByFechaIngresoAsync(DateTime fechaIngreso);
    Task<bool> InscripcionExistsAsync(int id);
}
