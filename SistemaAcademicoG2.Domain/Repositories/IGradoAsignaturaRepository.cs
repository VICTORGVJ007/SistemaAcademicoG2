using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAcademicoG2.Domain.Entities;
namespace SistemaAcademicoG2.Domain.Repositories
{
    public interface IGradoAsignaturaRepository
    {
        // CRUD básico
        Task<IEnumerable<GradoAsignatura>> GetAllAsync();
        Task<GradoAsignatura> GetByIdAsync(int id);
        Task<IEnumerable<GradoAsignatura>> GetByGradoIdAsync(int idGrado);
        Task<IEnumerable<GradoAsignatura>> GetByAsignaturaIdAsync(int idAsignatura);
        Task<IEnumerable<GradoAsignatura>> GetByEstadoAsync(int estado);
        Task AddAsync(GradoAsignatura entity);
        Task UpdateAsync(GradoAsignatura entity);
        Task DeleteAsync(int id);
        Task<bool> GradoAsignaturaExistsAsync(int id);

       
        
        

    }
}
