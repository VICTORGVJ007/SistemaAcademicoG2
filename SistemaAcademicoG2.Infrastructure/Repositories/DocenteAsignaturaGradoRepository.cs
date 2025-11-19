using SistemaAcademicoG2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoG2.Infrastructure.Data;

public class DocenteAsignaturaGradoRepository : IDocenteAsignaturaGradoRepository
{
    private readonly AppDBContext _context;

    public DocenteAsignaturaGradoRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DocenteAsignaturaGrado>> GetActivosAsync() =>
        await _context.DocenteAsignaturaGrados
            .Include(x => x.Usuario)
            .Include(x => x.Grado)
            .Include(x => x.Asignatura)
            .Where(x => x.Estado)
            .ToListAsync();

    public async Task<IEnumerable<DocenteAsignaturaGrado>> GetInactivosAsync() =>
        await _context.DocenteAsignaturaGrados
            .Include(x => x.Usuario)
            .Include(x => x.Grado)
            .Include(x => x.Asignatura)
            .Where(x => !x.Estado)
            .ToListAsync();

    public async Task<DocenteAsignaturaGrado?> GetByIdAsync(int id) =>
        await _context.DocenteAsignaturaGrados
            .Include(x => x.Usuario)
            .Include(x => x.Grado)
            .Include(x => x.Asignatura)
            .FirstOrDefaultAsync(x => x.IdDGA == id);

    public async Task AddAsync(DocenteAsignaturaGrado entidad)
    {
        await _context.DocenteAsignaturaGrados.AddAsync(entidad);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ActivarAsync(int id)
    {
        var item = await _context.DocenteAsignaturaGrados.FindAsync(id);
        if (item == null) return false;

        item.Estado = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DesactivarAsync(int id)
    {
        var item = await _context.DocenteAsignaturaGrados.FindAsync(id);
        if (item == null) return false;

        item.Estado = false;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExisteDuplicadoAsync(int idUsuario, int idGrado, int idAsignatura)
    {
        return await _context.DocenteAsignaturaGrados.AnyAsync(x =>
            x.IdUsuario == idUsuario &&
            x.IdGrado == idGrado &&
            x.IdAsignatura == idAsignatura
        );
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

