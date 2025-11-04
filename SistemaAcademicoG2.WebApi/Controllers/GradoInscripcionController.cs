using Microsoft.AspNetCore.Mvc;
using SistemaAcademicoG2.BL.Services;
using SistemaAcademicoG2.Domain.Entities;

namespace SistemaAcademicoG2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradoInscripcionController : ControllerBase
    {
        private readonly GradoInscripcionService _service;

        public GradoInscripcionController(GradoInscripcionService service)
        {
            _service = service;
        }

        // GET: api/GradoInscripcion
        [HttpGet]
        public async Task<IActionResult> Get() =>
            Ok(await _service.GetAllAsync());

        // GET: api/GradoInscripcion/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // GET: api/GradoInscripcion/Grado/3
        [HttpGet("Grado/{idGrado}")]
        public async Task<IActionResult> GetByGrado(int idGrado) =>
            Ok(await _service.GetByGradoIdAsync(idGrado));

        // GET: api/GradoInscripcion/Inscripcion/2
        [HttpGet("Inscripcion/{idInscripcion}")]
        public async Task<IActionResult> GetByInscripcion(int idInscripcion) =>
            Ok(await _service.GetByInscripcionIdAsync(idInscripcion));

        // GET: api/GradoInscripcion/Estado/1
        [HttpGet("Estado/{estado}")]
        public async Task<IActionResult> GetByEstado(int estado) =>
            Ok(await _service.GetByEstadoAsync(estado));

        // POST: api/GradoInscripcion
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GradoInscripcion model)
        {
            await _service.AddAsync(model);
            return CreatedAtAction(nameof(Get), new { id = model.IdGradoInscripcion }, model);
        }

        // PUT: api/GradoInscripcion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] GradoInscripcion model)
        {
            if (id != model.IdGradoInscripcion)
                return BadRequest("Id no coincide.");

            await _service.UpdateAsync(model);
            return NoContent();
        }

        // DELETE: api/GradoInscripcion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
