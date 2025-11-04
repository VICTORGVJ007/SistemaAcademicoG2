using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaAcademicoG2.Domain.Entities;

namespace SistemaAcademicoG2.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        // ==========================
        // CRUD básico
        // ==========================

        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);

        // ==========================
        // Métodos de búsqueda
        // ==========================

        Task<IEnumerable<Usuario>> GetByNombreAsync(string nombre);
        Task<Usuario?> GetByCorreoAsync(string correo);
        Task<Usuario?> ValidarLoginAsync(string correo, string password);
        Task<Usuario?> GetByUsernameAndPasswordAsync(string nombreUsuario, string clave);

        // ✅ Aquí está el método correcto
        Task<Usuario?> GetByEmailAsync(string correo);

        // ==========================
        // Validación
        // ==========================

        Task<bool> ExisteCorreoAsync(string correo);

        // ==========================
        // Utilidades
        // ==========================

        Task<int> CountAsync();
        Task AddUsuarioAsync(Usuario usuario);
    }
}
