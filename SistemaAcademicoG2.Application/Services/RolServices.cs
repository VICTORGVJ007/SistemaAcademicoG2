using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;

namespace SistemaAcademicoG2.Application.Services
{
    public class RolService
    {
        private readonly IRolRepository _repository;

        public RolService(IRolRepository repository)
        {
            _repository = repository;
        }

        // Caso de uso: Obtener roles activos
        public async Task<IEnumerable<Rol>> ObtenerRolesActivosAsync()
        {
            var roles = await _repository.GetAllAsync();
            return roles.Where(r => r.Estado.ToLower() == "activo");
        }

        // Caso de uso: Agregar rol (evitar duplicados por nombre)
        public async Task<string> AgregarRolAsync(Rol nuevoRol)
        {
            try
            {
                var existe = await _repository.RolExistsAsync(nuevoRol.Nombre);
                if (existe)
                    return "Error: Ya existe un rol con ese nombre";

                nuevoRol.Estado = "Activo"; // Estado por defecto
                await _repository.AddAsync(nuevoRol);
                return "Rol agregado correctamente";
            }
            catch (Exception ex)
            {
                return "Error de servidor: " + ex.Message;
            }

        }
    }
}
