using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Infrastructure.Data;
using SistemaAcademicoG2.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class GradoAsignaturaRepository : IGradoAsignaturaRepository
{
    private readonly AppDBContext _context;

    public GradoAsignaturaRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GradoAsignatura>> GetActivasAsync() =>
        await _context.GradoAsignaturas
            .Include(g => g.Grado)
            .Include(g => g.Asignatura)
            .Where(g => g.Estado)
            .ToListAsync();

    public async Task<IEnumerable<GradoAsignatura>> GetInactivasAsync() =>
        await _context.GradoAsignaturas
            .Include(g => g.Grado)
            .Include(g => g.Asignatura)
            .Where(g => !g.Estado)
            .ToListAsync();

    public async Task<GradoAsignatura?> GetByIdAsync(int id) =>
        await _context.GradoAsignaturas
            .Include(g => g.Grado)
            .Include(g => g.Asignatura)
            .FirstOrDefaultAsync(g => g.IdGradoAsignatura == id);

    public async Task AddAsync(GradoAsignatura entidad)
    {
        _context.GradoAsignaturas.Add(entidad);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(GradoAsignatura entidad)
    {
        _context.GradoAsignaturas.Update(entidad);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DesactivarAsync(int id)
    {
        var item = await _context.GradoAsignaturas.FindAsync(id);
        if (item == null) return false;
        item.Estado = false;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<GradoAsignatura>> GetAllAsync()
    {
        return await _context.GradoAsignaturas
            .Include(g => g.Grado)          // Carga la entidad Grado relacionada
            .Include(g => g.Asignatura)     // Carga la entidad Asignatura relacionada
            .OrderBy(g => g.IdGradoAsignatura) // Ordena por ID ascendente
            .ToListAsync();                 // Convierte el resultado a lista
    }


    public async Task<bool> ActivarAsync(int id)
    {
        var item = await _context.GradoAsignaturas.FindAsync(id);
        if (item == null) return false;
        item.Estado = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> GradoAsignaturaExistsAsync(int id) =>
        await _context.GradoAsignaturas.AnyAsync(g => g.IdGradoAsignatura == id);

    public async Task<IEnumerable<GradoAsignatura>> GetByGradoAsync(int idGrado)
    {
        return await _context.GradoAsignaturas
            .Where(ga => ga.IdGrado == idGrado)
            .Include(ga => ga.Asignatura)
            .ToListAsync();
    }

}
