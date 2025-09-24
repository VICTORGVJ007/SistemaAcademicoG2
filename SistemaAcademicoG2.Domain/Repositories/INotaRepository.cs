using SistemaAcademicoG2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Domain.Repositories
{
    // Contrato del repositorio de notas
    public interface INotaRepository
    {
        // Obtener todas las notas
        Task<IEnumerable<Nota>> GetNotasAsync();

        // Obtener una nota por su id
        Task<Nota> GetNotaByIdAsync(int id);

        // Agregar una nueva nota
        Task<Nota> AddNotaAsync(Nota nota);

        // Actualizar una nota existente
        Task<Nota> UpdateNotaAsync(Nota nota);

        // Eliminar una nota por su id
        Task<bool> DeleteNotaAsync(int id);
    }
}

