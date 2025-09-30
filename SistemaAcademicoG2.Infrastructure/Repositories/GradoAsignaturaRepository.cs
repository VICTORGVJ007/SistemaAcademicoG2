using Microsoft.EntityFrameworkCore;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;
using SistemaAcademicoG2.Infrastructure.Data;

namespace SistemaAcademicoG2.Infrastructure.Repositories
{
    public class GradoAsignaturaRepository : IGradoAsignaturaRepository
    {
        private readonly AppDBContext _context;

        public GradoAsignaturaRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GradoAsignatura>> GetAllAsync() =>
            await _context.GradoAsignaturas
                          .Include(g => g.Grado)
                          .Include(g => g.Asignatura)
                          .ToListAsync();

        public async Task<GradoAsignatura> GetByIdAsync(int id) =>
            await _context.GradoAsignaturas
                          .Include(g => g.Grado)
                          .Include(g => g.Asignatura)
                          .FirstOrDefaultAsync(g => g.IdGradoAsignatura == id);

        public async Task<IEnumerable<GradoAsignatura>> GetByGradoIdAsync(int idGrado) =>
            await _context.GradoAsignaturas
                          .Where(g => g.IdGrado == idGrado)
                          .Include(g => g.Grado)
                          .Include(g => g.Asignatura)
                          .ToListAsync();

        public async Task<IEnumerable<GradoAsignatura>> GetByAsignaturaIdAsync(int idAsignatura) =>
            await _context.GradoAsignaturas
                          .Where(g => g.IdAsignatura == idAsignatura)
                          .Include(g => g.Grado)
                          .Include(g => g.Asignatura)
                          .ToListAsync();

        public async Task<IEnumerable<GradoAsignatura>> GetByEstadoAsync(int estado) =>
            await _context.GradoAsignaturas
                          .Where(g => g.Estado == estado)
                          .Include(g => g.Grado)
                          .Include(g => g.Asignatura)
                          .ToListAsync();

        public async Task AddAsync(GradoAsignatura entity)
        {
            await _context.GradoAsignaturas.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(GradoAsignatura entity)
        {
            _context.GradoAsignaturas.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.GradoAsignaturas.FindAsync(id);
            if (entity != null)
            {
                _context.GradoAsignaturas.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> GradoAsignaturaExistsAsync(int id) =>
            await _context.GradoAsignaturas.AnyAsync(e => e.IdGradoAsignatura == id);
    }
}
