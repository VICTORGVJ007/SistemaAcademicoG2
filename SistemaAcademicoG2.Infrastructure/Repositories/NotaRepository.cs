using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;
using SistemaAcademicoG2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace SistemaAcademicoG2.Infrastructure.Repositories
{
    public class NotaRepository : INotaRepository
    {
        private readonly AppDBContext _context;

        public NotaRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Nota>> GetNotasAsync() =>
            await _context.Notas.ToListAsync();

        public async Task<Nota?> GetNotaByIdAsync(int id) =>
            await _context.Notas.FindAsync(id);

        public async Task<Nota> AddNotaAsync(Nota nota)
        {
            _context.Notas.Add(nota);
            await _context.SaveChangesAsync();
            return nota;
        }

        public async Task<Nota> UpdateNotaAsync(Nota nota)
        {
            _context.Notas.Update(nota);
            await _context.SaveChangesAsync();
            return nota;
        }

        // ===== SOFT DELETE =====
        public async Task<bool> DesactivarNotaAsync(int id)
        {
            var nota = await _context.Notas.FindAsync(id);

            if (nota == null)
                return false;

            nota.Estado = false; // Inactiva
            await _context.SaveChangesAsync();
            return true;
        }

        // ===== ACTIVAR =====
        public async Task<bool> ActivarNotaAsync(int id)
        {
            var nota = await _context.Notas.FindAsync(id);

            if (nota == null)
                return false;

            nota.Estado = true; // Activa
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> NotaExistsAsync(int id) =>
            await _context.Notas.AnyAsync(n => n.IdNota == id);
    }
}
