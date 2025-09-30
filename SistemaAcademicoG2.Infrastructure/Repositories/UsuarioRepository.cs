using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;
using SistemaAcademicoG2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDBContext _context;

    public UsuarioRepository(AppDBContext context)
    {
        _context = context;
    }

    // ==========================
    // CRUD básico
    // ==========================

    public async Task<IEnumerable<Usuario>> GetAllAsync() =>
        await _context.Usuarios.ToListAsync();

    public async Task<Usuario> GetByIdAsync(int id) =>
        await _context.Usuarios.FindAsync(id);

    public async Task AddAsync(Usuario usuario)
    {
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }

    // ==========================
    // Métodos de búsqueda
    // ==========================

    public async Task<IEnumerable<Usuario>> GetByNombreAsync(string nombre) =>
        await _context.Usuarios
            .Where(u => u.Nombre.Contains(nombre))
            .ToListAsync();

    public async Task<Usuario> GetByCorreoAsync(string correo) =>
        await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Correo == correo);

    public async Task<Usuario> ValidarLoginAsync(string correo, string clave) =>
        await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Correo == correo && u.Clave == clave);

    public async Task<Usuario?> GetByUsernameAndPasswordAsync(string nombreUsuario, string clave) =>
        await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Nombre == nombreUsuario && u.Clave == clave);

    // ==========================
    // Métodos de validación
    // ==========================

    public async Task<bool> ExisteCorreoAsync(string correo) =>
        await _context.Usuarios.AnyAsync(u => u.Correo == correo);

    // ==========================
    // Métodos de utilidades
    // ==========================

    public async Task<int> CountAsync() =>
        await _context.Usuarios.CountAsync();
}
