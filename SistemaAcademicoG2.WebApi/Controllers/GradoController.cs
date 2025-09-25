using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Infrastructure.Data;

namespace SistemaAcademicoG2.WebApi.Controllers
{
    [Route("api/Grado")]
    [ApiController]
    public class GradoController : ControllerBase
    {
        private readonly AppDBContext _context;

        public GradoController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Grado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grado>>> GetGrados()
        {
            return await _context.Grados.ToListAsync();
        }

        // GET: api/Grado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grado>> GetGrado(int id)
        {
            var grado = await _context.Grados.FindAsync(id);

            if (grado == null)
            {
                return NotFound();
            }

            return grado;
        }

        // POST: api/Grado
        [HttpPost]
        public async Task<ActionResult<Grado>> PostGrado(Grado grado)
        {
            _context.Grados.Add(grado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGrado", new { id = grado.IdGrado }, grado);
        }

        // PUT: api/Grado/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrado(int id, Grado grado)
        {
            if (id != grado.IdGrado)
            {
                return BadRequest();
            }

            _context.Entry(grado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradoExists(id))
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

        // DELETE: api/Grado/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrado(int id)
        {
            var grado = await _context.Grados.FindAsync(id);
            if (grado == null)
            {
                return NotFound();
            }

            _context.Grados.Remove(grado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GradoExists(int id)
        {
            return _context.Grados.Any(e => e.IdGrado == id);
        }
    }
}