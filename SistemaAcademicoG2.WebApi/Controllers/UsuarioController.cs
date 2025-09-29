using Microsoft.AspNetCore.Mvc;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // ==========================
        // GET: api/Usuario
        // ==========================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.ObtenerUsuariosActivosAsync();
            return Ok(usuarios);
        }

        // ==========================
        // GET: api/Usuario/5
        // ==========================
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _usuarioService.ObtenerUsuarioPorIdAsync(id);
            if (usuario == null)
                return NotFound("Usuario no encontrado o inactivo.");
            return Ok(usuario);
        }

        // ==========================
        // POST: api/Usuario
        // ==========================
        [HttpPost]
        public async Task<ActionResult> PostUsuario([FromBody] Usuario usuario)
        {
            var resultado = await _usuarioService.AgregarUsuarioAsync(usuario);

            if (resultado.StartsWith("Error"))
                return BadRequest(resultado);

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
        }

        // ==========================
        // PUT: api/Usuario/5
        // ==========================
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutUsuario(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
                return BadRequest("El ID no coincide con el usuario enviado.");

            var resultado = await _usuarioService.ModificarUsuarioAsync(usuario);

            if (resultado.StartsWith("Error"))
                return NotFound(resultado);

            return NoContent();
        }

        // ==========================
        // DELETE (Soft Delete): api/Usuario/5
        // ==========================
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteUsuario(int id)
        {
            var resultado = await _usuarioService.EliminarUsuarioAsync(id);

            if (resultado.StartsWith("Error"))
                return NotFound(resultado);

            return NoContent();
        }

        // ==========================
        // POST: api/Usuario/login
        // ==========================
        [HttpPost("login")]
        public async Task<ActionResult<Usuario>> Login([FromBody] LoginRequest request)
        {
            var usuario = await _usuarioService.AutenticarAsync(request.NombreUsuario, request.Clave);

            if (usuario == null)
                return Unauthorized("Usuario o contraseña incorrectos.");

            return Ok(usuario);
        }
    }

    // Clase de request para login
    public class LoginRequest
    {
        public string NombreUsuario { get; set; }
        public string Clave { get; set; }
    }
}
