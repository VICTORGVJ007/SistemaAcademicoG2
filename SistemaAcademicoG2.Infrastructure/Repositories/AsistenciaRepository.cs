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

    // 🔹 Obtener todas las asistencias
    public async Task<IEnumerable<Asistencia>> GetAllAsync() =>
        await _context.Asistencias.ToListAsync();

    // 🔹 Obtener asistencia por ID
    public async Task<Asistencia> GetByIdAsync(int id) =>
        await _context.Asistencias.FirstOrDefaultAsync(a => a.IdAsistencia == id);

    // 🔹 Agregar una nueva asistencia
    public async Task AddAsync(Asistencia asistencia)
    {
        _context.Asistencias.Add(asistencia);
        await _context.SaveChangesAsync();
    }

    // 🔹 Actualizar una asistencia existente
    public async Task UpdateAsync(Asistencia asistencia)
    {
        _context.Asistencias.Update(asistencia);
        await _context.SaveChangesAsync();
    }

    // 🔹 Eliminar una asistencia por ID
    public async Task DeleteAsync(int id)
    {
        var asistencia = await _context.Asistencias.FindAsync(id);
        if (asistencia != null)
        {
            _context.Asistencias.Remove(asistencia);
            await _context.SaveChangesAsync();
        }
    }

    // 🔹 Obtener asistencias por fecha
    public async Task<IEnumerable<Asistencia>> GetByFechaAsync(DateTime fecha) =>
        await _context.Asistencias
            .Where(a => a.Fecha.Date == fecha.Date)
            .ToListAsync();

    // ✅ Obtener asistencias por ID de usuario
    public async Task<IEnumerable<Asistencia>> GetByUsuarioAsync(int idUsuario) =>
        await _context.Asistencias
            .Where(a => a.IdUsuario == idUsuario)
            .ToListAsync();

    // 🔹 Verificar si una asistencia existe
    public async Task<bool> AsistenciaExistsAsync(int id) =>
        await _context.Asistencias.AnyAsync(a => a.IdAsistencia == id);
}
