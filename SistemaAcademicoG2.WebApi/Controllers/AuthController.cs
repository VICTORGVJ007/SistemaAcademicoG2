using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaAcademicoG2.Application.Services;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    // -----------------------------
    // Registro
    // -----------------------------
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var (ok, msg) = await _authService.RegisterAsync(
            dto.Nombre,
            dto.Apellido,
            dto.Correo,
            dto.Password,
            dto.IdRol
        );

        if (!ok)
            return BadRequest(new { message = msg });

        return Ok(new { message = msg });
    }

    // -----------------------------
    // Login
    // -----------------------------
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var (ok, tokenOrMsg) = await _authService.LoginAsync(dto.Correo, dto.Password);

        if (!ok)
            return Unauthorized(new { message = tokenOrMsg });

        return Ok(new { token = tokenOrMsg });
    }

    // -----------------------------
    // Datos del token
    // -----------------------------
    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
        => Ok(User.Claims.Select(c => new { c.Type, c.Value }));
}

// -----------------------------
// DTOs
// -----------------------------
public class RegisterDto
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Correo { get; set; }
    public string Password { get; set; }
    public int IdRol { get; set; }
}

public class LoginDto
{
    public string Correo { get; set; }
    public string Password { get; set; }
}
