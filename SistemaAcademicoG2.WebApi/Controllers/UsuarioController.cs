using Microsoft.AspNetCore.Mvc;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaAcademicoG2.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // ============================
        // GET: api/Usuario
        // ============================
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();
            return Ok(usuarios);
        }

        // ============================
        // GET: api/Usuario/5
        // ============================
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        // ============================
        // POST: api/Usuario
        // ============================
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario([FromBody] Usuario usuario)
        {
            await _usuarioRepository.AddAsync(usuario);
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.IdUsuario }, usuario);
        }

        // ============================
        // PUT: api/Usuario/5
        // ============================
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.IdUsuario)
                return BadRequest("El ID no coincide con el usuario enviado.");

            await _usuarioRepository.UpdateAsync(usuario);
            return NoContent();
        }

        // ============================
        // DELETE: api/Usuario/5
        // ============================
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
                return NotFound();

            await _usuarioRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
