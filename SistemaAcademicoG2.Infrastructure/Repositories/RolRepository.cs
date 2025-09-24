using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;
using SistemaAcademicoG2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class RolRepository : IRolRepository
{
    private readonly AppDBContext _context;

    public RolRepository(AppDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Rol>> GetAllAsync() =>
        await _context.Roles.ToListAsync();

    public async Task<Rol> GetByIdAsync(int id) =>
        await _context.Roles.FindAsync(id);
     
    public async Task AddAsync(Rol rol)
    {
        _context.Roles.Add(rol);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Rol rol)
    {
        _context.Roles.Update(rol);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var rol = await _context.Roles.FindAsync(id);
        if (rol != null)
        {
            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Rol>> GetByEstadoAsync(string estado) =>
        await _context.Roles.Where(r => r.Estado == estado).ToListAsync();

    public async Task<bool> RolExistsAsync(int id) =>
        await _context.Roles.AnyAsync(r => r.Id == id);

    public async Task<bool> RolExistsAsync(string nombre) =>
        await _context.Roles.AnyAsync(r => r.Nombre == nombre);
}
