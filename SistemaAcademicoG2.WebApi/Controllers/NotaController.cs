using Microsoft.AspNetCore.Mvc;
using SistemaAcademicoG2.Application.Services;
using SistemaAcademicoG2.WebApi.DTOs;
using SistemaAcademicoG2.Domain.Entities;
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

        // ============================================
        // LISTAR NOTAS ACTIVAS
        // ============================================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotaDTO>>> GetActivas()
        {
            return Ok(await _notaService.ObtenerNotasActivasAsync());
        }

        // ============================================
        // LISTAR NOTAS INACTIVAS
        // ============================================
        [HttpGet("inactivas")]
        public async Task<ActionResult<IEnumerable<NotaDTO>>> GetInactivas()
        {
            return Ok(await _notaService.ObtenerNotasInactivasAsync());
        }

        // ============================================
        // OBTENER POR ID
        // ============================================
        [HttpGet("{id}")]
        public async Task<ActionResult<NotaDTO>> GetById(int id)
        {
            var nota = await _notaService.ObtenerNotaPorIdAsync(id);

            if (nota == null)
                return NotFound($"No existe nota con ID {id}.");

            return Ok(nota);
        }

        // ============================================
        // CREAR
        // ============================================
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NotaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // ===== MAPEO DTO → ENTITY =====
            var nota = new Nota
            {
                IdNota = dto.IdNota,
                IdUsuario = dto.IdUsuario,
                IdAsignatura = dto.IdAsignatura,
                IdPeriodo = dto.IdPeriodo,
                Nota1 = dto.Nota1,
                Nota2 = dto.Nota2,
                Nota3 = dto.Nota3,
                PromedioFinal = dto.PromedioFinal,
                EstadoAcademico = dto.EstadoAcademico,
                Estado = dto.Estado,
            };

            var msg = await _notaService.AgregarNotaAsync(nota);
            return msg.StartsWith("Error") ? BadRequest(msg) : Ok(msg);
        }

        // ============================================
        // MODIFICAR
        // ============================================
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] NotaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // ===== MAPEO DTO → ENTITY =====
            var nota = new Nota
            {
                IdNota = id,
                IdUsuario = dto.IdUsuario,
                IdAsignatura = dto.IdAsignatura,
                IdPeriodo = dto.IdPeriodo,
                Nota1 = dto.Nota1,
                Nota2 = dto.Nota2,
                Nota3 = dto.Nota3,
                PromedioFinal = dto.PromedioFinal,
                EstadoAcademico = dto.EstadoAcademico,
                Estado = dto.Estado,
            };

            var msg = await _notaService.ModificarNotaAsync(nota);
            return msg.StartsWith("Error") ? BadRequest(msg) : Ok(msg);
        }

        // ============================================
        // DESACTIVAR
        // ============================================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var msg = await _notaService.DesactivarNotaAsync(id);
            return msg.StartsWith("Error") ? BadRequest(msg) : Ok(msg);
        }
    }
}
