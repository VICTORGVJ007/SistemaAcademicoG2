using SistemaAcademicoG2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Domain.Repositories
{
    public interface IGradoRepository
    {
        // CRUD básico
        Task<IEnumerable<Grado>> GetAllAsync();
        Task<Grado> GetByIdAsync(int id);
        Task AddAsync(Grado grado);
        Task UpdateAsync(Grado grado);
        Task DeleteAsync(int id);

        // Extras útiles
        Task<IEnumerable<Grado>> GetByNombreAsync(string nombre);
        Task<bool> GradoExistsAsync(int id);
    }
}