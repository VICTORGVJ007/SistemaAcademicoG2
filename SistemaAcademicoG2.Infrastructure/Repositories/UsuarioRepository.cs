using Microsoft.EntityFrameworkCore;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;
using SistemaAcademicoG2.Infrastructure.Data;

namespace SistemaAcademicoG2.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDBContext _context;

        public UsuarioRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
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

        public async Task<IEnumerable<Usuario>> GetByNombreAsync(string nombre)
        {
            return await _context.Usuarios
                .Where(u => u.Nombre.Contains(nombre))
                .ToListAsync();
        }

        public async Task<Usuario?> GetByCorreoAsync(string correo)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo);
        }

        public async Task<Usuario?> GetByEmailAsync(string correo)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo);
        }

        public async Task<Usuario?> ValidarLoginAsync(string correo, string password)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo && u.PasswordHash == password);
        }

        public async Task<Usuario?> GetByUsernameAndPasswordAsync(string nombreUsuario, string clave)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Nombre == nombreUsuario && u.PasswordHash == clave);
        }

        public async Task<bool> ExisteCorreoAsync(string correo)
        {
            return await _context.Usuarios.AnyAsync(u => u.Correo == correo);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Usuarios.CountAsync();
        }

        public async Task AddUsuarioAsync(Usuario usuario)
        {
            await AddAsync(usuario);
        }

        public async Task<IEnumerable<Usuario>> GetByRolAsync(int idRol)
        {
            return await _context.Usuarios
                .Where(u => u.IdRol == idRol && u.Estado)
                .ToListAsync();
        }

    }
}
