using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.API.DTOs;
using SistemaAcademicoG2.Application.Services;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.DTOs;

namespace SistemaAcademicoG2.WebApi.Controllers
{
    [Route("api/gradoasignatura")]
    [ApiController]
    public class GradoAsignaturaController : ControllerBase
    {
        private readonly GradoAsignaturaService _service;

        public GradoAsignaturaController(GradoAsignaturaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<GradoAsignaturaDTO>>> GetActivas()
        {
            var lista = await _service.ObtenerActivasAsync();

            var dtoList = lista.Select(g => new GradoAsignaturaDTO
            {
                IdGradoAsignatura = g.IdGradoAsignatura,
                IdGrado = g.IdGrado,
                NombreGrado = g.Grado?.Nombre ?? "Sin grado",
                IdAsignatura = g.IdAsignatura,
                NombreAsignatura = g.Asignatura?.Nombre ?? "Sin asignatura",
                Estado = g.Estado
            }).ToList();

            return Ok(dtoList);
        }

        [HttpPost("multiple")]
        public async Task<IActionResult> CrearMultiple([FromBody] GradoAsignaturaMultipleDTO dto)
        {
            if (dto.IdGrado == 0 || dto.IdAsignaturas == null || !dto.IdAsignaturas.Any())
                return BadRequest("Debe seleccionar un grado y al menos una asignatura.");

            var resultado = await _service.AgregarMultipleAsync(dto);
            return Ok(resultado);
        }



        [HttpGet("inactivas")]
        public async Task<ActionResult<List<GradoAsignaturaDTO>>> GetInactivas()
        {
            var lista = await _service.ObtenerInactivasAsync();

            var dtoList = lista.Select(g => new GradoAsignaturaDTO
            {
                IdGradoAsignatura = g.IdGradoAsignatura,
                IdGrado = g.IdGrado,
                NombreGrado = g.Grado?.Nombre ?? "Sin grado",
                IdAsignatura = g.IdAsignatura,
                NombreAsignatura = g.Asignatura?.Nombre ?? "Sin asignatura",
                Estado = g.Estado
            }).ToList();

            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GradoAsignaturaDTO>> GetPorId(int id)
        {
            var item = await _service.ObtenerPorIdAsync(id);
            if (item == null) return NotFound("Registro no encontrado.");

            var dto = new GradoAsignaturaDTO
            {
                IdGradoAsignatura = item.IdGradoAsignatura,
                IdGrado = item.IdGrado,
                NombreGrado = item.Grado?.Nombre ?? "Sin grado",
                IdAsignatura = item.IdAsignatura,
                NombreAsignatura = item.Asignatura?.Nombre ?? "Sin asignatura",
                Estado = item.Estado
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GradoAsignatura entidad)
        {
            entidad.Estado = true;
            var resultado = await _service.AgregarGradoAsignaturaAsync(entidad);
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, GradoAsignatura entidad)
        {
            entidad.IdGradoAsignatura = id;
            var resultado = await _service.ModificarAsync(entidad);
            return Ok(resultado);
        }

        [HttpPut("activar/{id}")]
        public async Task<IActionResult> Activar(int id)
        {
            var resultado = await _service.ActivarAsync(id);
            if (resultado.StartsWith("Error")) return NotFound(resultado);
            return NoContent();
        }

        [HttpPut("desactivar/{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var resultado = await _service.DesactivarAsync(id);
            if (resultado.StartsWith("Error")) return NotFound(resultado);
            return NoContent();
        }
    }
}
