using Microsoft.AspNetCore.Mvc;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Application.Services;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.WebApi.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<IEnumerable<Nota>>> Get()
        {
            var notas = await _notaService.ObtenerNotasActivasAsync();
            return Ok(notas);
        }

        // GET: api/nota/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nota>> GetById(int id)
        {
            try
            {
                var nota = await _notaService.ObtenerNotaPorIdAsync(id);
                if (nota == null)
                    return NotFound($"No se encontró una nota activa con ID {id}");

                return Ok(nota);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/nota
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

        // PUT: api/nota/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Nota nota)
        {
            try
            {
                nota.Id = id; // Aseguramos que use el id de la ruta
                var resultado = await _notaService.ModificarNotaAsync(nota);

                if (resultado.StartsWith("Error"))
                    return BadRequest(resultado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/nota/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

