// Archivo: SistemaAcademicoG2.Controllers/AsignaturaController.cs
using Microsoft.AspNetCore.Mvc;
using SistemaAcademicoG2.Application.Services;
using SistemaAcademicoG2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.Controllers
{
    [Route("api/asignatura")]
    [ApiController]
    public class AsignaturaController : ControllerBase
    {
        private readonly AsignaturaService _asignaturaService;

        public AsignaturaController(AsignaturaService asignaturaService)
        {
            _asignaturaService = asignaturaService;
        }

        // GET: api/asignatura
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asignatura>>> Get()
        {
            var asignaturas = await _asignaturaService.ObtenerAsignaturasActivasAsync();
            return Ok(asignaturas);
        }

        // GET: api/asignatura/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asignatura>> GetById(int id)
        {
            try
            {
                var asignatura = await _asignaturaService.ObtenerAsignaturaPorIdAsync(id);

                if (asignatura == null)
                    return NotFound($"No se encontró una asignatura activa con ID {id}");

                return Ok(asignatura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // POST: api/asignatura
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Asignatura asignatura)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var resultado = await _asignaturaService.AgregarAsignaturaAsync(asignatura);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return Ok(resultado);
        }

        // PUT: api/asignatura/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Asignatura asignatura)
        {
            try
            {
                asignatura.Id = id;
                var resultado = await _asignaturaService.ModificarAsignaturaAsync(asignatura);

                if (resultado.StartsWith("Error"))
                    return BadRequest(resultado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        // DELETE: api/asignatura/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}


