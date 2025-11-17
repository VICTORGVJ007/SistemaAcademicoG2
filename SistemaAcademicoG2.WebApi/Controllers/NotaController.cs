using Microsoft.AspNetCore.Mvc;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.WebApi.Controllers
{
    [Route("api/nota")]
    [ApiController]
    public class NotaController : ControllerBase
    {
        private readonly NotaService _notaService;

        public NotaController(NotaService notaService)
        {
            _notaService = notaService;
        }

        // GET: api/nota
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nota>>> GetActivas()
        {
            var notas = await _notaService.ObtenerNotasActivasAsync();
            return Ok(notas);
        }

        // GET: api/nota/inactivas
        [HttpGet("inactivas")]
        public async Task<ActionResult<IEnumerable<Nota>>> GetInactivas()
        {
            var notas = await _notaService.ObtenerNotasInactivasAsync();
            return Ok(notas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Nota>> GetById(int id)
        {
            var nota = await _notaService.ObtenerNotaPorIdAsync(id);

            if (nota == null)
                return NotFound($"No se encontró una nota activa con ID {id}");

            return Ok(nota);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Nota nota)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _notaService.AgregarNotaAsync(nota);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Nota nota)
        {
            nota.IdNota = id;

            var resultado = await _notaService.ModificarNotaAsync(nota);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var respuesta = await _notaService.DesactivarNotaAsync(id);

            if (respuesta.StartsWith("Error"))
                return BadRequest(respuesta);

            return Ok(respuesta);
        }
    }
}
