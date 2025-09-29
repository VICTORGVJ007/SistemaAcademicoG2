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
    public class NotaRepository : INotaRepository
    {
        private readonly AppDBContext _context;

        public NotaRepository(AppDBContext context)
        {
            _context = context;
        }

        // Obtener todas las notas
        public async Task<IEnumerable<Nota>> GetNotasAsync() =>
            await _context.Notas.ToListAsync();

        // Obtener una nota por su Id
        public async Task<Nota?> GetNotaByIdAsync(int id) =>
            await _context.Notas.FindAsync(id);

        // Agregar una nueva nota
        public async Task<Nota> AddNotaAsync(Nota nota)
        {
            _context.Notas.Add(nota);
            await _context.SaveChangesAsync();
            return nota;
        }

        // Actualizar una nota existente
        public async Task<Nota> UpdateNotaAsync(Nota nota)
        {
            _context.Notas.Update(nota);
            await _context.SaveChangesAsync();
            return nota;
        }

        // Eliminar una nota por Id
        public async Task<bool> DeleteNotaAsync(int id)
        {
            var nota = await _context.Notas.FindAsync(id);
            if (nota != null)
            {
                _context.Notas.Remove(nota);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // Buscar notas por período (opcional)
        public async Task<IEnumerable<Nota>> GetByPeriodoAsync(string periodo) =>
            await _context.Notas
                          .Where(n => n.Periodo.Contains(periodo))
                          .ToListAsync();

        // Verificar si una nota existe por Id
        public async Task<bool> NotaExistsAsync(int id) =>
            await _context.Notas.AnyAsync(n => n.Id == id);
    }
}
