using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AsistenciaRepository : IAsistenciaRepository
{
    private readonly AppDBContext _context;

    public AsistenciaRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Asistencia>> GetAllAsync() =>
        await _context.Asistencias.ToListAsync();

    public async Task<IEnumerable<Asistencia>> GetActivasAsync() =>
        await _context.Asistencias.Where(a => a.Estado).ToListAsync();

    public async Task<IEnumerable<Asistencia>> GetInactivasAsync() =>
        await _context.Asistencias.Where(a => !a.Estado).ToListAsync();

    public async Task<Asistencia> GetByIdAsync(int id) =>
        await _context.Asistencias.FirstOrDefaultAsync(a => a.IdAsistencia == id);

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

    public async Task<bool> DesactivarAsync(int id)
    {
        var asistencia = await _context.Asistencias.FindAsync(id);
        if (asistencia == null) return false;

        asistencia.Estado = false;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ActivarAsync(int id)
    {
        var asistencia = await _context.Asistencias.FindAsync(id);
        if (asistencia == null) return false;

        asistencia.Estado = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Asistencia>> GetByFechaAsync(DateTime fecha) =>
        await _context.Asistencias
            .Where(a => a.Fecha.Date == fecha.Date)
            .ToListAsync();

    public async Task<IEnumerable<Asistencia>> GetByUsuarioAsync(int idUsuario) =>
        await _context.Asistencias
            .Where(a => a.IdUsuario == idUsuario)
            .ToListAsync();

    public async Task<bool> AsistenciaExistsAsync(int id) =>
        await _context.Asistencias.AnyAsync(a => a.IdAsistencia == id);
}

