using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;
using SistemaAcademicoG2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class PeriodoRepository : IPeriodoRepository
{
    private readonly AppDBContext _context;

    public PeriodoRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Periodo>> GetActivosAsync() =>
        await _context.Periodos.Where(p => p.Estado).ToListAsync();

    public async Task<IEnumerable<Periodo>> GetInactivosAsync() =>
        await _context.Periodos.Where(p => !p.Estado).ToListAsync();

    public async Task<Periodo> GetByIdAsync(int id) =>
        await _context.Periodos.FirstOrDefaultAsync(p => p.IdPeriodo == id);

    public async Task AddAsync(Periodo periodo)
    {
        _context.Periodos.Add(periodo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Periodo periodo)
    {
        _context.Periodos.Update(periodo);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DesactivarAsync(int id)
    {
        var periodo = await _context.Periodos.FindAsync(id);
        if (periodo == null) return false;

        periodo.Estado = false;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ActivarAsync(int id)
    {
        var periodo = await _context.Periodos.FindAsync(id);
        if (periodo == null) return false;

        periodo.Estado = true;
        await _context.SaveChangesAsync();
        return true;
    }
}
