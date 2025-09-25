using SistemaAcademico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Domain.Repositories
{
    public interface IInscripcionRepository
    {
        // CRUD básico
        Task<IEnumerable<Inscripcion>> GetAllAsync();
        Task<Inscripcion> GetByIdAsync(int id);
        Task AddAsync(Inscripcion inscripcion);
        Task UpdateAsync(Inscripcion inscripcion);
        Task DeleteAsync(int id);

        // Extras útiles
        Task<IEnumerable<Inscripcion>> GetByUsuarioIdAsync(int usuarioId);
        Task<IEnumerable<Inscripcion>> GetByGradoIdAsync(int gradoId);
        Task<IEnumerable<Inscripcion>> GetByFechaIngresoAsync(DateTime fechaIngreso);
        Task<bool> InscripcionExistsAsync(int id);
    }
}
