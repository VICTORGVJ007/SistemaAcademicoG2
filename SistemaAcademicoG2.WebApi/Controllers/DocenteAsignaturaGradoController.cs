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

        // GET TODOS ACTIVOS
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocenteAsignaturaGradoDTO>>> Get()
        {
            try
            {
                var list = await _service.ObtenerActivosAsync();

                var dtoList = list.Select(d => new DocenteAsignaturaGradoDTO
                {
                    IdDGA = d.IdDGA,
                    IdUsuario = d.IdUsuario,
                    NombreDocente = d.Usuario?.Nombre ?? string.Empty,
                    IdGradoAsignatura = d.IdGradoAsignatura,
                    NombreGrado = d.GradoAsignatura?.Grado?.Nombre ?? string.Empty,
                    NombreAsignatura = d.GradoAsignatura?.Asignatura?.Nombre ?? string.Empty,
                    Estado = d.Estado
                }).ToList();

                return Ok(dtoList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al obtener las asignaciones: {ex.Message}");
            }
        }

        // GET POR ID
        [HttpGet("{id}")]
        public async Task<ActionResult<DocenteAsignaturaGradoDTO>> GetPorId(int id)
        {
            try
            {
                var item = await _service.ObtenerPorIdAsync(id);
                if (item == null)
                    return NotFound("Registro no encontrado.");

                var dto = new DocenteAsignaturaGradoDTO
                {
                    IdDGA = item.IdDGA,
                    IdUsuario = item.IdUsuario,
                    NombreDocente = item.Usuario?.Nombre ?? string.Empty,
                    IdGradoAsignatura = item.IdGradoAsignatura,
                    NombreGrado = item.GradoAsignatura?.Grado?.Nombre ?? string.Empty,
                    NombreAsignatura = item.GradoAsignatura?.Asignatura?.Nombre ?? string.Empty,
                    Estado = item.Estado
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al obtener la asignación: {ex.Message}");
            }
        }

        // CREAR
        [HttpPost]
        public async Task<IActionResult> Crear(DocenteAsignaturaGradoCrearDTO dto)
        {
            try
            {
                var entidad = new DocenteAsignaturaGrado
                {
                    IdUsuario = dto.IdUsuario,
                    IdGradoAsignatura = dto.IdGradoAsignatura,
                    Estado = true
                };

                var resultado = await _service.AgregarAsync(entidad);

                if (resultado.StartsWith("Error"))
                    return BadRequest(resultado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al crear la asignación: {ex.Message}");
            }
        }

        // EDITAR
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, DocenteAsignaturaGradoDTO dto)
        {
            try
            {
                if (id != dto.IdDGA)
                    return BadRequest("El Id no coincide.");

                var entidad = new DocenteAsignaturaGrado
                {
                    IdDGA = dto.IdDGA,
                    IdUsuario = dto.IdUsuario,
                    IdGradoAsignatura = dto.IdGradoAsignatura,
                    Estado = dto.Estado
                };

                var resultado = await _service.ActualizarAsync(entidad);

                if (resultado.StartsWith("Error"))
                    return NotFound(resultado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al editar la asignación: {ex.Message}");
            }
        }

        // ACTIVAR
        [HttpPut("activar/{id}")]
        public async Task<IActionResult> Activar(int id)
        {
            try
            {
                var resultado = await _service.ActivarAsync(id);
                if (resultado.StartsWith("Error"))
                    return NotFound(resultado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al activar la asignación: {ex.Message}");
            }
        }

        // DESACTIVAR
        [HttpPut("desactivar/{id}")]
        public async Task<IActionResult> Desactivar(int id)
        {
            try
            {
                var resultado = await _service.DesactivarAsync(id);
                if (resultado.StartsWith("Error"))
                    return NotFound(resultado);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al desactivar la asignación: {ex.Message}");
            }
        }

        [HttpGet("asignaturas/por-grado/{idGrado}")]
        public async Task<ActionResult<IEnumerable<GradoAsignaturaDTO>>> ObtenerAsignaturasPorGrado(int idGrado)
        {
            try
            {
                var lista = await _service.ObtenerAsignaturasPorGradoAsync(idGrado);

                var dtoList = lista.Select(g => new GradoAsignaturaDTO
                {
                    IdGradoAsignatura = g.IdGradoAsignatura,
                    IdGrado = g.IdGrado,
                    IdAsignatura = g.IdAsignatura,
                    NombreGrado = g.Grado?.Nombre ?? string.Empty,
                    NombreAsignatura = g.Asignatura?.Nombre ?? string.Empty,
                    Estado = g.Estado
                }).ToList();

                return Ok(dtoList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error al obtener las asignaturas: {ex.Message}");
            }
        }
    }
}
