using Microsoft.AspNetCore.Mvc;
using SistemaAcademicoG2.Application.Services;
using SistemaAcademicoG2.Domain.Entities;

namespace SistemaAcademicoG2.Controllers
{
    [Route("api/periodo")]
    [ApiController]
    public class PeriodoController : ControllerBase
    {
        private readonly PeriodoServices _service;

        public PeriodoController(PeriodoServices service)
        {
            _service = service;
        }

        // Obtener activos
        [HttpGet]
        public async Task<IActionResult> GetActivos()
        {
            return Ok(await _service.ObtenerActivosAsync());
        }

        // Obtener inactivos
        [HttpGet("inactivos")]
        public async Task<IActionResult> GetInactivos()
        {
            return Ok(await _service.ObtenerInactivosAsync());
        }

        // Obtener por id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var periodo = await _service.ObtenerPorIdAsync(id);
            if (periodo == null) return NotFound();

            return Ok(periodo);
        }

        // Agregar
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Periodo periodo)
        {
            var result = await _service.AgregarAsync(periodo);
            return Ok(result);
        }

        // Modificar
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Periodo periodo)
        {
            periodo.IdPeriodo = id;
            var result = await _service.ModificarAsync(periodo);
            return Ok(result);
        }

        // Desactivar
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DesactivarAsync(id);
            return Ok(result);
        }

        // Activar
        [HttpPut("activar/{id}")]
        public async Task<IActionResult> Activar(int id)
        {
            var result = await _service.ActivarAsync(id);
            return Ok(result);
        }
    }
}

