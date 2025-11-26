//using SistemaAcademico.Domain.Entities;
//using SistemaAcademicoG2.Domain.Repositories;
//using SistemaAcademicoG2.Infrastructure.Data;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace SistemaAcademicoG2.Infrastructure.Repositories
//{
//    public class InscripcionRepository : IInscripcionRepository
//    {
//        private readonly AppDBContext _context;

//        public InscripcionRepository(AppDBContext context)
//        {
//            _context = context;
//        }

//        public async Task<IEnumerable<Inscripcion>> GetAllAsync() =>
//            await _context.Inscripciones.ToListAsync();

//        public async Task<Inscripcion?> GetByIdAsync(int id) =>
//            await _context.Inscripciones.FindAsync(id);

//        public async Task AddAsync(Inscripcion inscripcion)
//        {
//            _context.Inscripciones.Add(inscripcion);
//            await _context.SaveChangesAsync();
//        }

//        public async Task UpdateAsync(Inscripcion inscripcion)
//        {
//            _context.Inscripciones.Update(inscripcion);
//            await _context.SaveChangesAsync();
//        }

//        public async Task CambiarEstadoAsync(int id)
//        {
//            var inscripcion = await _context.Inscripciones.FindAsync(id);
//            if (inscripcion != null)
//            {
//                inscripcion.Estado = !inscripcion.Estado;
//                await _context.SaveChangesAsync();
//            }
//        }


//        //  Métodos con propiedades reales de la entidad
//        public async Task<IEnumerable<Inscripcion>> GetByFechaIngresoAsync(DateTime fecha) =>
//            await _context.Inscripciones
//                .Where(i => i.FechaIngreso.Date == fecha.Date)
//                .ToListAsync();

//        public async Task<IEnumerable<Inscripcion>> GetByUsuarioIdAsync(int idUsuario) =>
//            await _context.Inscripciones
//                .Where(i => i.IdUsuario == idUsuario)
//                .ToListAsync();

//        public async Task<IEnumerable<Inscripcion>> GetByGradoIdAsync(int idGrado) =>
//            await _context.Inscripciones
//                .Where(i => i.IdGrado == idGrado)
//                .ToListAsync();

//        public async Task<IEnumerable<Inscripcion>> GetByEstadoAsync(bool estado) =>
//            await _context.Inscripciones
//                .Where(i => i.Estado == estado)
//                .ToListAsync();

//        public async Task<bool> InscripcionExistsAsync(int id) =>
//            await _context.Inscripciones.AnyAsync(i => i.IdInscripcion == id);
//    }
//}
