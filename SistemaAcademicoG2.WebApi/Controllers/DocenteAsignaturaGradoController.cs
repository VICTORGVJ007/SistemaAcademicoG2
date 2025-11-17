using Microsoft.AspNetCore.Mvc;
using SistemaAcademicoG2.Domain.Entities;

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
    public async Task<IActionResult> GetActivos()
    {
        return Ok(await _service.ObtenerActivosAsync());
    }

    [HttpGet("inactivos")]
    public async Task<IActionResult> GetInactivos()
    {
        return Ok(await _service.ObtenerInactivosAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPorId(int id)
    {
        var entidad = await _service.ObtenerPorIdAsync(id);
        if (entidad == null) return NotFound();

        return Ok(entidad);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] DocenteAsignaturaGrado entidad)
    {
        var result = await _service.AgregarAsync(entidad);
        if (result.StartsWith("Error")) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] DocenteAsignaturaGrado entidad)
    {
        entidad.IdDGA = id;
        var result = await _service.ModificarAsync(entidad);

        if (result.StartsWith("Error")) return BadRequest(result);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DesactivarAsync(id);
        if (result.StartsWith("Error")) return BadRequest(result);

        return Ok(result);
    }

    [HttpPut("activar/{id}")]
    public async Task<IActionResult> Activar(int id)
    {
        var result = await _service.ActivarAsync(id);
        if (result.StartsWith("Error")) return BadRequest(result);

        return Ok(result);
    }
}
