using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAcademicoG2.Domain.Entities;

namespace SistemaAcademicoG2.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        // Definir métodos para operaciones CRUD
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task<Usuario> GetByIdAsync(int id);
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);
        Task<Usuario> GetByIdAsync(string id);
        Task<Usuario> GetByUsernameAsync(string username);
        Task<Usuario> GetByUsernameAndPasswordAsync(string username, string password);
        Task<bool> UserExistsAsync(string username);
        Task<bool> UserExistsAsync(int id);
    }
}
