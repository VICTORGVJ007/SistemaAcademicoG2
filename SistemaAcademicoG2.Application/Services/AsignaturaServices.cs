using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Referencia
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;

namespace SistemaAcademicoG2.Application.Services
{
    // Algoritmos con lógica de negocio (Use Case)
    public class AsignaturaService
    {
        private readonly IAsignaturaRepository _repository;

        public AsignaturaService(IAsignaturaRepository repository)
        {
            _repository = repository;
        }

        // Caso de uso: Buscar una asignatura por Id (solo activas)
        public async Task<Asignatura?> ObtenerAsignaturaPorIdAsync(int id)
        {
            if (id <= 0)
                return null; // Id no válido

            var asignatura = await _repository.GetAsignaturaByIdAsync(id);

            if (asignatura != null && asignatura.Estado == true)
                return asignatura;

            return null; // No encontrada o inactiva
        }

        // Caso de uso: Modificar una asignatura
        public async Task<string> ModificarAsignaturaAsync(Asignatura asignatura)
        {
            if (asignatura.Id <= 0)
                return "Error: Id no válido";

            var existente = await _repository.GetAsignaturaByIdAsync(asignatura.Id);
            if (existente == null)
                return "Error: Asignatura no encontrada";

            // Actualizamos solo los campos necesarios
            existente.Nombre = asignatura.Nombre;
            existente.Estado = asignatura.Estado; // Permitir cambiar estado

            await _repository.UpdateAsignaturaAsync(existente);

            return "Asignatura modificada correctamente";
        }

        // Caso de uso: Obtener solo asignaturas activas
        public async Task<IEnumerable<Asignatura>> ObtenerAsignaturasActivasAsync()
        {
            var asignaturas = await _repository.GetAsignaturasAsync();
            return asignaturas.Where(a => a.Estado == true);
        }

        // Caso de uso: Agregar asignatura (validar duplicado por nombre)
        public async Task<string> AgregarAsignaturaAsync(Asignatura nuevaAsignatura)
        {
            try
            {
                var asignaturas = await _repository.GetAsignaturasAsync();

                if (asignaturas.Any(a => a.Nombre.ToLower() == nuevaAsignatura.Nombre.ToLower()))
                    return "Error: ya existe una asignatura con el mismo nombre";

                nuevaAsignatura.Estado = true; // Activa por defecto

                var insertada = await _repository.AddAsignaturaAsync(nuevaAsignatura);

                if (insertada == null || insertada.Id <= 0)
                    return "Error: no se pudo agregar la asignatura";

                return "Asignatura agregada correctamente";
            }
            catch (Exception ex)
            {
                return $"Error de servidor: {ex.Message}";
            }
        }
    }
}

