using Microsoft.EntityFrameworkCore;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;
using SistemaAcademicoG2.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Infrastructure.Repositories
{
    public class GradoInscripcionRepository : IGradoInscripcionRepository
    {
        private readonly AppDBContext _context;
        private readonly GradoRepository _gradoRepository;

        public GradoInscripcionRepository(AppDBContext context, GradoRepository gradoRepository)
        {
            _context = context;
            _gradoRepository = gradoRepository; // ✅ Tu dependencia original se respeta
        }

        // ===========================================================
        // ✅ Obtener todos
        // ===========================================================
        public async Task<IEnumerable<GradoInscripcion>> GetAllAsync() =>
            await _context.GradoInscripciones
                .Include(g => g.Grado)
                .Include(g => g.Inscripcion)
                .ToListAsync();

        // ===========================================================
        // ✅ Obtener por ID
        // ===========================================================
        public async Task<GradoInscripcion?> GetByIdAsync(int id) =>
            await _context.GradoInscripciones
                .Include(g => g.Grado)
                .Include(g => g.Inscripcion)
                .FirstOrDefaultAsync(g => g.IdGradoInscripcion == id);

        // ===========================================================
        // ✅ Obtener por IdGrado
        // ===========================================================
        public async Task<IEnumerable<GradoInscripcion>> GetByGradoIdAsync(int idGrado) =>
            await _context.GradoInscripciones
                .Where(g => g.IdGrado == idGrado)
                .Include(g => g.Grado)
                .Include(g => g.Inscripcion)
                .ToListAsync();

        // ===========================================================
        // ✅ Obtener por IdInscripcion
        // ===========================================================
        public async Task<IEnumerable<GradoInscripcion>> GetByInscripcionIdAsync(int idInscripcion) =>
            await _context.GradoInscripciones
                .Where(g => g.IdInscripcion == idInscripcion)
                .Include(g => g.Grado)
                .Include(g => g.Inscripcion)
                .ToListAsync();

        // ===========================================================
        // ✅ Obtener por estado
        // ===========================================================
        public async Task<IEnumerable<GradoInscripcion>> GetByEstadoAsync(int estado) =>
            await _context.GradoInscripciones
                .Where(g => g.Estado == estado)
                .Include(g => g.Grado)
                .Include(g => g.Inscripcion)
                .ToListAsync();

        // ===========================================================
        // ✅ Crear
        // ===========================================================
        public async Task AddAsync(GradoInscripcion entity)
        {
            // ✅ Se obtiene el grado desde el repositorio externo si lo necesitas
            entity.Grado = await _gradoRepository.GetByIdAsync(entity.IdGrado);

            await _context.GradoInscripciones.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // ===========================================================
        // ✅ Actualizar
        // ===========================================================
        public async Task UpdateAsync(GradoInscripcion entity)
        {
            var exists = await GradoInscripcionExistsAsync(entity.IdGradoInscripcion);

            if (!exists)
                throw new KeyNotFoundException("La relación Grado–Inscripción no existe.");

            _context.GradoInscripciones.Update(entity);
            await _context.SaveChangesAsync();
        }

        // ===========================================================
        // ✅ Eliminar
        // ===========================================================
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.GradoInscripciones.FindAsync(id);

            if (entity == null)
                throw new KeyNotFoundException("No se encontró el registro para eliminar.");

            _context.GradoInscripciones.Remove(entity);
            await _context.SaveChangesAsync();
        }

        // ===========================================================
        // ✅ Verificar existencia
        // ===========================================================
        public async Task<bool> GradoInscripcionExistsAsync(int id) =>
            await _context.GradoInscripciones.AnyAsync(g => g.IdGradoInscripcion == id);
    }
}
