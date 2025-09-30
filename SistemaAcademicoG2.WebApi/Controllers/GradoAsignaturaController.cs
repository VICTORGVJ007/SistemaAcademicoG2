using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.WebApi.Controllers
{
    [Route("api/GradoAsignatura")]
    [ApiController]
    public class GradoAsignaturaController : ControllerBase
    {
        private readonly AppDBContext _context;

        public GradoAsignaturaController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/GradoAsignatura
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GradoAsignatura>>> GetAll()
        {
            return await _context.GradoAsignaturas
                                 .Include(ga => ga.Grado)
                                 .Include(ga => ga.Asignatura)
                                 .ToListAsync();
        }

        // GET: api/GradoAsignatura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GradoAsignatura>> GetById(int id)
        {
            var gradoAsignatura = await _context.GradoAsignaturas
                                                .Include(ga => ga.Grado)
                                                .Include(ga => ga.Asignatura)
                                                .FirstOrDefaultAsync(ga => ga.IdGradoAsignatura == id);

            if (gradoAsignatura == null)
            {
                return NotFound();
            }

            return gradoAsignatura;
        }

        // POST: api/GradoAsignatura
        [HttpPost]
        public async Task<ActionResult<GradoAsignatura>> Create(GradoAsignatura gradoAsignatura)
        {
            _context.GradoAsignaturas.Add(gradoAsignatura);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = gradoAsignatura.IdGradoAsignatura }, gradoAsignatura);
        }

        // PUT: api/GradoAsignatura/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GradoAsignatura gradoAsignatura)
        {
            if (id != gradoAsignatura.IdGradoAsignatura)
            {
                return BadRequest("El ID no coincide.");
            }

            _context.Entry(gradoAsignatura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradoAsignaturaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/GradoAsignatura/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var gradoAsignatura = await _context.GradoAsignaturas.FindAsync(id);
            if (gradoAsignatura == null)
            {
                return NotFound();
            }

            _context.GradoAsignaturas.Remove(gradoAsignatura);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GradoAsignaturaExists(int id)
        {
            return _context.GradoAsignaturas.Any(e => e.IdGradoAsignatura == id);
        }
    }
}
