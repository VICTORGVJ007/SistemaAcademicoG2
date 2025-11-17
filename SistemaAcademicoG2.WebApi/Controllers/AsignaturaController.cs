using Microsoft.AspNetCore.Mvc;
using SistemaAcademicoG2.Application.Services;
using SistemaAcademicoG2.Domain.Entities;

namespace SistemaAcademicoG2.Controllers
{
    [Route("api/asignatura")]
    [ApiController]
    public class AsignaturaController : ControllerBase
    {
        private readonly AsignaturaService _service;

        public AsignaturaController(AsignaturaService service)
        {
            _service = service;
        }

        // Obtener solo activas
        [HttpGet]
        public async Task<IActionResult> GetActivas()
        {
            return Ok(await _service.ObtenerActivasAsync());
        }

        // Obtener solo inactivas
        [HttpGet("inactivas")]
        public async Task<IActionResult> GetInactivas()
        {
            return Ok(await _service.ObtenerInactivasAsync());
        }

        // Obtener por Id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var asignatura = await _service.ObtenerPorIdAsync(id);
            if (asignatura == null) return NotFound();

            return Ok(asignatura);
        }

        // Agregar
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Asignatura asignatura)
        {
            var result = await _service.AgregarAsync(asignatura);
            if (result.StartsWith("Error")) return BadRequest(result);

            return Ok(result);
        }

        // Modificar
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Asignatura asignatura)
        {
            asignatura.IdAsignatura = id;
            var result = await _service.ModificarAsync(asignatura);
            if (result.StartsWith("Error")) return BadRequest(result);

            return Ok(result);
        }

        // Desactivar (soft delete)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DesactivarAsync(id);
            if (result.StartsWith("Error")) return BadRequest(result);

            return Ok(result);
        }

        // Activar
        [HttpPut("activar/{id}")]
        public async Task<IActionResult> Activar(int id)
        {
            var result = await _service.ActivarAsync(id);
            if (result.StartsWith("Error")) return BadRequest(result);

            return Ok(result);
        }
    }
}



