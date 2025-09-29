using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Referencias
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;
using SistemaAcademicoG2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoG2.Infrastructure.Repositories
{
    public class AsignaturaRepository : IAsignaturaRepository
    {
        private readonly AppDBContext _context;

        public AsignaturaRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Asignatura>> GetAsignaturasAsync() =>
            await _context.Asignaturas.ToListAsync();

        public async Task<Asignatura?> GetAsignaturaByIdAsync(int id) =>
            await _context.Asignaturas.FindAsync(id);

        public async Task<Asignatura> AddAsignaturaAsync(Asignatura asignatura)
        {
            _context.Asignaturas.Add(asignatura);
            await _context.SaveChangesAsync();
            return asignatura;
        }

        public async Task<Asignatura> UpdateAsignaturaAsync(Asignatura asignatura)
        {
            _context.Asignaturas.Update(asignatura);
            await _context.SaveChangesAsync();
            return asignatura;
        }

        public async Task<bool> DeleteAsignaturaAsync(int id)
        {
            var asignatura = await _context.Asignaturas.FindAsync(id);
            if (asignatura != null)
            {
                _context.Asignaturas.Remove(asignatura);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // búsqueda por nombre
        public async Task<IEnumerable<Asignatura>> GetByNombreAsync(string nombre) =>
            await _context.Asignaturas
                          .Where(a => a.Nombre.Contains(nombre))
                          .ToListAsync();

        // Comprobar si una asignatura existe por Id
        public async Task<bool> AsignaturaExistsAsync(int id) =>
            await _context.Asignaturas.AnyAsync(a => a.Id == id);
    }
}

