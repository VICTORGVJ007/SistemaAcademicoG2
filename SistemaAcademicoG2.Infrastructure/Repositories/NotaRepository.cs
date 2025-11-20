using Microsoft.EntityFrameworkCore;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;
using SistemaAcademicoG2.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Infrastructure.Repositories
{
    public class NotaRepository :INotaRepository
    {
        private readonly AppDBContext _context;

        public NotaRepository(AppDBContext context)
        {
            _context = context;
        }

        // ==========================================
        // OBTENER TODAS LAS NOTAS CON RELACIONES
        // ==========================================
        public async Task<IEnumerable<Nota>> GetNotasWithIncludesAsync()
        {
            return await _context.Notas
                .Include(n => n.Usuario)
                .Include(n => n.Asignatura)
                .Include(n => n.Periodo)
                .ToListAsync();
        }

        // ==========================================
        // OBTENER NOTA POR ID CON RELACIONES
        // ==========================================
        public async Task<Nota?> GetNotaByIdWithIncludesAsync(int id)
        {
            return await _context.Notas
                .Include(n => n.Usuario)
                .Include(n => n.Asignatura)
                .Include(n => n.Periodo)
                .FirstOrDefaultAsync(n => n.IdNota == id);
        }

        // ==========================================
        // AGREGAR NOTA
        // ==========================================
        public async Task AddNotaAsync(Nota nota)
        {
            await _context.Notas.AddAsync(nota);
            await _context.SaveChangesAsync();
        }

        // ==========================================
        // ACTUALIZAR NOTA
        // ==========================================
        public async Task UpdateNotaAsync(Nota nota)
        {
            _context.Notas.Update(nota);
            await _context.SaveChangesAsync();
        }

        // ==========================================
        // DESACTIVAR NOTA (Estado = false)
        // ==========================================
        public async Task<bool> DesactivarNotaAsync(int id)
        {
            var nota = await _context.Notas.FindAsync(id);

            if (nota == null)
                return false;

            nota.Estado = false;
            await _context.SaveChangesAsync();
            return true;
        }

        // ==========================================
        // VERIFICAR SI EXISTE NOTA
        // ==========================================
        public async Task<bool> NotaExistsAsync(int id)
        {
            return await _context.Notas.AnyAsync(n => n.IdNota == id);
        }
    }
}
