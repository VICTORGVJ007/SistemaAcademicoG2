using System.Collections.Generic;
using System.Threading.Tasks;
using SistemaAcademicoG2.Domain.Entities;

namespace SistemaAcademicoG2.Domain.Repositories
{
    /// <summary>
    /// Define los métodos de acceso a datos para la entidad Usuario.
    /// </summary>
    public interface IUsuarioRepository
    {
        // ==========================
        // CRUD básico
        // ==========================

        /// <summary>
        /// Obtiene todos los usuarios.
        /// </summary>
        Task<IEnumerable<Usuario>> GetAllAsync();

        /// <summary>
        /// Obtiene un usuario por su ID.
        /// </summary>
        Task<Usuario> GetByIdAsync(int id);

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        Task AddAsync(Usuario usuario);

        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        Task UpdateAsync(Usuario usuario);

        /// <summary>
        /// Elimina un usuario por su ID.
        /// </summary>
        Task DeleteAsync(int id);

        // ==========================
        // Métodos de búsqueda
        // ==========================

        /// <summary>
        /// Busca usuarios cuyo nombre contenga la cadena indicada.
        /// </summary>
        Task<IEnumerable<Usuario>> GetByNombreAsync(string nombre);

        /// <summary>
        /// Obtiene un usuario por su correo.
        /// </summary>
        Task<Usuario> GetByCorreoAsync(string correo);

        /// <summary>
        /// Valida las credenciales de login (correo y contraseña).
        /// </summary>
        Task<Usuario> ValidarLoginAsync(string correo, string password);

        /// <summary>
        /// Obtiene un usuario por nombre de usuario y contraseña.
        /// </summary>
        Task<Usuario?> GetByUsernameAndPasswordAsync(string nombreUsuario, string clave);

        // ==========================
        // Métodos de validación
        // ==========================

        /// <summary>
        /// Verifica si existe un usuario con el correo indicado.
        /// </summary>
        Task<bool> ExisteCorreoAsync(string correo);

        // ==========================
        // Métodos de utilidades
        // ==========================

        /// <summary>
        /// Devuelve la cantidad de usuarios registrados.
        /// </summary>
        Task<int> CountAsync();
    }
}
