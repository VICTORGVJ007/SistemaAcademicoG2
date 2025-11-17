using Microsoft.EntityFrameworkCore;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Infrastructure.Data;

namespace SistemaAcademicoG2.DAL.Repositories
{
    public class DocenteAsignaturaGradoRepository : IDocenteAsignaturaGradoRepository
    {
        private readonly AppDBContext _context;

        public DocenteAsignaturaGradoRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DocenteAsignaturaGrado>> GetActivosAsync()
        {
            return await _context.DocenteAsignaturaGrados
                .Where(x => x.Estado)
                .ToListAsync();
        }

        public async Task<IEnumerable<DocenteAsignaturaGrado>> GetInactivosAsync()
        {
            return await _context.DocenteAsignaturaGrados
                .Where(x => !x.Estado)
                .ToListAsync();
        }

        public async Task<DocenteAsignaturaGrado?> GetByIdAsync(int id)
        {
            return await _context.DocenteAsignaturaGrados
                .FirstOrDefaultAsync(x => x.IdDGA == id);
        }

        public async Task<bool> ExisteRelacionAsync(int idUsuario, int idGrado, int idAsignatura)
        {
            return await _context.DocenteAsignaturaGrados.AnyAsync(x =>
                x.IdUsuario == idUsuario &&
                x.IdGrado == idGrado &&
                x.IdAsignatura == idAsignatura);
        }

        public async Task AddAsync(DocenteAsignaturaGrado entidad)
        {
            _context.DocenteAsignaturaGrados.Add(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DocenteAsignaturaGrado entidad)
        {
            _context.DocenteAsignaturaGrados.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DesactivarAsync(int id)
        {
            var entidad = await _context.DocenteAsignaturaGrados.FindAsync(id);
            if (entidad == null) return false;

            entidad.Estado = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ActivarAsync(int id)
        {
            var entidad = await _context.DocenteAsignaturaGrados.FindAsync(id);
            if (entidad == null) return false;

            entidad.Estado = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
