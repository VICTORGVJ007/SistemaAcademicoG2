using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaAcademico.Domain.Entities;
using SistemaAcademicoG2.Infrastructure.Data;
using SistemaAcademicoG2.WebApi.DTOs;

namespace SistemaAcademicoG2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionController : ControllerBase
    {
        private readonly AppDBContext _context;

        public InscripcionController(AppDBContext context)
        {
            _context = context;
        }

        // ================================================
        // GET: api/Inscripcion
        // ================================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InscripcionDTO>>> GetInscripciones()
        {
            var data = await _context.Inscripciones
                .AsNoTracking()
                .Include(i => i.Usuario)
                .Include(i => i.Grado)
                .Select(i => new InscripcionDTO
                {
                    IdInscripcion = i.IdInscripcion,
                    FechaIngreso = i.FechaIngreso,
                    Estado = i.Estado,

                    IdUsuario = i.IdUsuario,
                    NombreUsuario = i.Usuario.Nombre,

                    IdGrado = i.IdGrado,
                    NombreGrado = i.Grado.Nombre
                })
                .ToListAsync();

            return Ok(data);
        }

        // ================================================
        // GET: api/Inscripcion/5
        // ================================================
        [HttpGet("{id}")]
        public async Task<ActionResult<InscripcionDTO>> GetInscripcion(int id)
        {
            var inscripcion = await _context.Inscripciones
                .AsNoTracking()
                .Include(i => i.Usuario)
                .Include(i => i.Grado)
                .Where(i => i.IdInscripcion == id)
                .Select(i => new InscripcionDTO
                {
                    IdInscripcion = i.IdInscripcion,
                    FechaIngreso = i.FechaIngreso,
                    Estado = i.Estado,

                    IdUsuario = i.IdUsuario,
                    NombreUsuario = i.Usuario.Nombre,

                    IdGrado = i.IdGrado,
                    NombreGrado = i.Grado.Nombre
                })
                .FirstOrDefaultAsync();

            if (inscripcion == null)
                return NotFound(new { message = "Inscripción no encontrada." });

            return Ok(inscripcion);
        }

        // ================================================
        // POST
        // ================================================
        [HttpPost]
        public async Task<ActionResult> PostInscripcion([FromBody] InscripcionDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var inscripcion = new Inscripcion
            {
                FechaIngreso = dto.FechaIngreso,
                Estado = dto.Estado,
                IdUsuario = dto.IdUsuario,
                IdGrado = dto.IdGrado
            };

            _context.Inscripciones.Add(inscripcion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInscripcion),
                new { id = inscripcion.IdInscripcion }, dto);
        }

        // ================================================
        // PUT
        // ================================================
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInscripcion(int id, [FromBody] InscripcionDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != dto.IdInscripcion)
                return BadRequest(new { message = "El ID no coincide." });

            var inscripcion = await _context.Inscripciones.FindAsync(id);

            if (inscripcion == null)
                return NotFound(new { message = "Inscripción no encontrada." });

            inscripcion.FechaIngreso = dto.FechaIngreso;
            inscripcion.Estado = dto.Estado;
            inscripcion.IdUsuario = dto.IdUsuario;
            inscripcion.IdGrado = dto.IdGrado;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ================================================
        // DELETE
        // ================================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInscripcion(int id)
        {
            var inscripcion = await _context.Inscripciones.FindAsync(id);

            if (inscripcion == null)
                return NotFound(new { message = "No existe la inscripción." });

            _context.Inscripciones.Remove(inscripcion);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}