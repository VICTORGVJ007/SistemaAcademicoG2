using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class AsistenciaRepository : IAsistenciaRepository
{
    private readonly AppDBContext _context;

    public AsistenciaRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Asistencia>> GetAllAsync() =>
        await _context.Asistencias.ToListAsync();

    public async Task<Asistencia> GetByIdAsync(int id) =>
        await _context.Asistencias.FindAsync(id);

    public async Task AddAsync(Asistencia asistencia)
    {
        _context.Asistencias.Add(asistencia);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Asistencia asistencia)
    {
        _context.Asistencias.Update(asistencia);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var asistencia = await _context.Asistencias.FindAsync(id);
        if (asistencia != null)
        {
            _context.Asistencias.Remove(asistencia);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Asistencia>> GetByFechaAsync(DateTime fecha) =>
        await _context.Asistencias.Where(a => a.Fecha.Date == fecha.Date).ToListAsync();

    public async Task<IEnumerable<Asistencia>> GetByNombreAsync(string nombre) =>
        await _context.Asistencias.Where(a => a.Nombre.Contains(nombre)).ToListAsync();

    public async Task<bool> AsistenciaExistsAsync(int id) =>
        await _context.Asistencias.AnyAsync(a => a.Id == id);
}
 