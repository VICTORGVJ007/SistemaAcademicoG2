using Microsoft.AspNetCore.Mvc;
using Proyecto.API.DTOs;
using SistemaAcademicoG2.Application.Services;
using SistemaAcademicoG2.Domain.Entities;

namespace SistemaAcademicoG2.WebApi.Controllers
{
    [Route("api/docente-asignatura-grado")]
    [ApiController]
    public class DocenteAsignaturaGradoController : ControllerBase
    {
        private readonly DocenteAsignaturaGradoService _service;

        public DocenteAsignaturaGradoController(DocenteAsignaturaGradoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<DocenteAsignaturaGradoDTO>>> Get()
        {
            var list = await _service.ObtenerActivosAsync();

            var dtoList = list.Select(d => new DocenteAsignaturaGradoDTO
            {
                IdDGA = d.IdDGA,
                IdUsuario = d.IdUsuario,
                NombreDocente = d.Usuario?.Nombre ?? "Sin nombre",
                IdGrado = d.IdGrado,
                NombreGrado = d.Grado?.Nombre ?? "Sin grado",
                IdAsignatura = d.IdAsignatura,
                NombreAsignatura = d.Asignatura?.Nombre ?? "Sin asignatura",
                Estado = d.Estado
            }).ToList();

            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocenteAsignaturaGradoDTO>> GetPorId(int id)
        {
            var item = await _service.ObtenerPorIdAsync(id);
            if (item == null)
                return NotFound("Registro no encontrado.");

            var dto = new DocenteAsignaturaGradoDTO
            {
                IdDGA = item.IdDGA,
                IdUsuario = item.IdUsuario,
                NombreDocente = item.Usuario?.Nombre ?? "Sin nombre",
                IdGrado = item.IdGrado,
                NombreGrado = item.Grado?.Nombre ?? "Sin grado",
                IdAsignatura = item.IdAsignatura,
                NombreAsignatura = item.Asignatura?.Nombre ?? "Sin asignatura",
                Estado = item.Estado
            };

            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, DocenteAsignaturaGradoDTO dto)
        {
            if (id != dto.IdDGA)
                return BadRequest("El id no coincide.");

            var entidad = new DocenteAsignaturaGrado
            {
                IdDGA = dto.IdDGA,
                IdUsuario = dto.IdUsuario,
                IdGrado = dto.IdGrado,
                IdAsignatura = dto.IdAsignatura,
                Estado = dto.Estado
            };

            var resultado = await _service.ActualizarAsync(entidad);
            if (resultado.StartsWith("Error"))
                return NotFound(resultado);

            return NoContent();
        }

        [HttpPut("activar/{id}")]
        public async Task<IActionResult> Activar(int id)
        {
            var resultado = await _service.ActivarAsync(id);
            if (resultado.StartsWith("Error"))
                return NotFound(resultado);

            return NoContent();
        }

        [HttpPut("desactivar/{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            var resultado = await _service.DesactivarAsync(id);
            if (resultado.StartsWith("Error"))
                return NotFound(resultado);

            return NoContent();
        }
    }
}
