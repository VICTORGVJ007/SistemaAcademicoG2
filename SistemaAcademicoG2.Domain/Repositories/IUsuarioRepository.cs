using System;
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
        // ========================
        // CRUD BÁSICO
        // ========================

        /// <summary>
        /// Obtiene todos los usuarios registrados.
        /// </summary>
        Task<IEnumerable<Usuario>> GetAllAsync();

        /// <summary>
        /// Obtiene un usuario por su identificador numérico.
        /// </summary>
        Task<Usuario?> GetByIdAsync(int id);

        /// <summary>
        /// Agrega un nuevo usuario.
        /// </summary>
        Task AddAsync(Usuario usuario);

        /// <summary>
        /// Actualiza los datos de un usuario existente.
        /// </summary>
        Task UpdateAsync(Usuario usuario);

        /// <summary>
        /// Elimina un usuario por su identificador.
        /// </summary>
        Task DeleteAsync(int id);

        // ========================
        // MÉTODOS DE BÚSQUEDA
        // ========================

        /// <summary>
        /// Obtiene un usuario por su identificador de cadena (ej. GUID, código).
        /// </summary>
        Task<Usuario?> GetByIdAsync(string id);

        /// <summary>
        /// Obtiene un usuario por su nombre de usuario.
        /// </summary>
        Task<Usuario?> GetByUsernameAsync(string username);

        /// <summary>
        /// Obtiene un usuario validando nombre de usuario y contraseña.
        /// </summary>
        Task<Usuario?> GetByUsernameAndPasswordAsync(string username, string password);

        // ========================
        // VALIDACIONES
        // ========================

        /// <summary>
        /// Verifica si existe un usuario con el nombre de usuario especificado.
        /// </summary>
        Task<bool> UserExistsAsync(string username);

        /// <summary>
        /// Verifica si existe un usuario con el identificador numérico especificado.
        /// </summary>
        Task<bool> UserExistsAsync(int id);

        /// <summary>
        /// Verifica si un usuario ya existe en base a su información completa.
        /// </summary>
        Task<bool> UserExistsAsync(Usuario usuario);
    }
}
