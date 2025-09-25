using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;
using SistemaAcademicoG2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Infrastructure.Repositories
{
    public class GradoRepository : IGradoRepository
    {
        private readonly AppDBContext _context;

        public GradoRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Grado>> GetAllAsync() =>
            await _context.Grados.ToListAsync();

        public async Task<Grado?> GetByIdAsync(int id) =>
            await _context.Grados.FindAsync(id);

        public async Task AddAsync(Grado grado)
        {
            _context.Grados.Add(grado);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Grado grado)
        {
            _context.Grados.Update(grado);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var grado = await _context.Grados.FindAsync(id);
            if (grado != null)
            {
                _context.Grados.Remove(grado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Grado>> GetByNombreAsync(string nombre) =>
            await _context.Grados
                .Where(g => g.Nombre.Contains(nombre))
                .ToListAsync();

        public async Task<bool> GradoExistsAsync(int id) =>
            await _context.Grados.AnyAsync(g => g.IdGrado == id);
    }
}
