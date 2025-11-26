using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Application.Services
{
    /// <summary>
    /// Servicio con lógica de negocio para la entidad Usuario.
    /// </summary>
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        // ==========================
        // Obtener usuario por ID
        // ==========================
        public async Task<Usuario?> ObtenerUsuarioPorIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var usuario = await _repository.GetByIdAsync(id);
            if (usuario != null && usuario.Estado)
                return usuario;

            return null;
        }

        // ==========================
        // Obtener todos los usuarios activos
        // ==========================
        public async Task<IEnumerable<Usuario>> ObtenerUsuariosActivosAsync()
        {
            var usuarios = await _repository.GetAllAsync();
            return usuarios.Where(u => u.Estado);
        }

        // ==========================
        // Agregar usuario
        // ==========================
        public async Task<string> AgregarUsuarioAsync(Usuario nuevoUsuario)
        {
            try
            {
                // Validar duplicados por nombre o correo
                var usuarios = await _repository.GetAllAsync();
                if (usuarios.Any(u => u.Nombre.ToLower() == nuevoUsuario.Nombre.ToLower() ||
                                      u.Correo.ToLower() == nuevoUsuario.Correo.ToLower()))
                {
                    return "Error: ya existe un usuario con el mismo nombre o correo";
                }

                nuevoUsuario.Estado = true; // activo por defecto
                await _repository.AddAsync(nuevoUsuario);
                return "Usuario agregado correctamente";
            }
            catch (Exception ex)
            {
                return $"Error de servidor: {ex.Message}";
            }
        }

        // ==========================
        // Modificar usuario
        // ==========================
        public async Task<string> ModificarUsuarioAsync(Usuario usuario)
        {
            try
            {
                if (usuario.IdUsuario <= 0)
                    return "Error: Id no válido";

                var existente = await _repository.GetByIdAsync(usuario.IdUsuario);
                if (existente == null)
                    return "Error: Usuario no encontrado";

                existente.Nombre = usuario.Nombre;
                existente.Apellido = usuario.Apellido;
                existente.Password = usuario.Password;
                existente.Correo = usuario.Correo;
                existente.IdRol = usuario.IdRol;
                existente.Estado = usuario.Estado;

                await _repository.UpdateAsync(existente);
                return "Usuario modificado correctamente";
            }
            catch (Exception ex)
            {
                return $"Error de servidor: {ex.Message}";
            }
        }

        // ==========================
        // Eliminar usuario (soft delete)
        // ==========================
        public async Task<string> EliminarUsuarioAsync(int idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                    return "Error: Id no válido";

                var usuario = await _repository.GetByIdAsync(idUsuario);
                if (usuario == null)
                    return "Error: Usuario no encontrado";

                usuario.Estado = false;
                await _repository.UpdateAsync(usuario);
                return "Usuario eliminado correctamente";
            }
            catch (Exception ex)
            {
                return $"Error de servidor: {ex.Message}";
            }
        }

        // ==========================
        // Login / Autenticación
        // ==========================
        public async Task<Usuario?> AutenticarAsync(string nombreUsuario, string clave)
        {
            if (string.IsNullOrEmpty(nombreUsuario) || string.IsNullOrEmpty(clave))
                return null;

            var usuario = await _repository.GetByUsernameAndPasswordAsync(nombreUsuario, clave);

            if (usuario != null && usuario.Estado)
                return usuario;

            return null;
        }

        public async Task<IEnumerable<Usuario>> ObtenerEstudiantesAsync()
        {
            const int RolEstudiante = 1; // Ajusta según tu tabla
            var usuarios = await _repository.GetByRolAsync(RolEstudiante);
            return usuarios;
        }

        public async Task<IEnumerable<Usuario>> ObtenerDocentesAsync()
        {
            const int RolDocente = 3; // Ajusta según tu tabla
            var usuarios = await _repository.GetByRolAsync(RolDocente);
            return usuarios;
        }

        // ==========================
        // Validaciones
        // ==========================
        public async Task<bool> ExisteCorreoAsync(string correo)
        {
            return await _repository.ExisteCorreoAsync(correo);
        }

        public async Task<int> ContarUsuariosAsync()
        {
            return await _repository.CountAsync();
        }

        public async Task<IEnumerable<Usuario>> BuscarPorNombreAsync(string nombre)
        {
            var usuarios = await _repository.GetByNombreAsync(nombre);
            return usuarios.Where(u => u.Estado); // Solo activos
        }

        public async Task<Usuario?> BuscarPorCorreoAsync(string correo)
        {
            var usuario = await _repository.GetByCorreoAsync(correo);
            if (usuario != null && usuario.Estado)
                return usuario;
            return null;
        }
        public async Task<Usuario?> ObtenerPorEmailAsync(string correo)
        {
            var usuario = await _repository.GetByEmailAsync(correo);
            if (usuario != null && usuario.Estado)
                return usuario;
            return null;
        }
        public async Task<Usuario?> ValidarLoginAsync(string correo, string password)
        {
            var usuario = await _repository.ValidarLoginAsync(correo, password);
            if (usuario != null && usuario.Estado)
                return usuario;
            return null;
        }
    }
}
