using Microsoft.AspNetCore.Mvc;
using SistemaAcademicoG2.Application.Services;
using SistemaAcademicoG2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.WebApi.Controllers
{
    [Route("api/asistencia")]
    [ApiController]
    public class AsistenciaController : ControllerBase
    {
        private readonly AsistenciaService _service;

        public AsistenciaController(AsistenciaService service)
        {
            _service = service;
        }

        // GET: api/asistencia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asistencia>>> GetActivas()
        {
            var lista = await _service.ObtenerActivasAsync();
            return Ok(lista);
        }

        // GET: api/asistencia/inactivas
        [HttpGet("inactivas")]
        public async Task<ActionResult<IEnumerable<Asistencia>>> GetInactivas()
        {
            return Ok(await _service.ObtenerInactivasAsync());
        }

        // GET: api/asistencia/fecha/2025-11-17
        [HttpGet("fecha/{fecha}")]
        public async Task<IActionResult> GetByFecha(DateTime fecha)
        {
            return Ok(await _service.ObtenerPorFechaAsync(fecha));
        }

        // POST: api/asistencia
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Asistencia asistencia)
        {
            var respuesta = await _service.AgregarAsistenciaAsync(asistencia);
            return Ok(respuesta);
        }

        // PUT: api/asistencia/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Asistencia asistencia)
        {
            asistencia.IdAsistencia = id;
            var respuesta = await _service.ModificarAsync(asistencia);
            return Ok(respuesta);
        }

        // DELETE (NO ELIMINA — DESACTIVA)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            return Ok(await _service.DesactivarAsync(id));
        }

        // PUT: api/asistencia/activar/5
        [HttpPut("activar/{id}")]
        public async Task<IActionResult> Activar(int id)
        {
            return Ok(await _service.ActivarAsync(id));
        }
    }
}

