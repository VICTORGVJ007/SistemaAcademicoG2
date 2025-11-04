using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SysProducto.Aplication.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository _repo;
        private readonly IConfiguration _cfg;

        public AuthService(IUsuarioRepository repo, IConfiguration cfg)
        {
            _repo = repo;
            _cfg = cfg;
        }

        // ============================
        // ✅ REGISTRO
        // ============================
        public async Task<(bool ok, string msg)> RegisterAsync(string nombre, string apellido, string correo, string clave, int rolId)
        {
            var existing = await _repo.GetByEmailAsync(correo);
            if (existing != null)
                return (false, "El correo ya está registrado");

            var hash = BCrypt.Net.BCrypt.HashPassword(clave);

            var usuario = new Usuario
            {
                Nombre = nombre,
                Apellido = apellido,
                Correo = correo,
                Password = hash,
                IdRol = rolId,
                Estado = true
            };

            await _repo.AddUsuarioAsync(usuario);

            return (true, "Usuario registrado correctamente");
        }

        // ============================
        // ✅ LOGIN
        // ============================
        public async Task<(bool ok, string tokenOrMsg)> LoginAsync(string correo, string password)
        {
            var user = await _repo.GetByEmailAsync(correo);
            if (user is null)
                return (false, "Credenciales inválidas");

            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                return (false, "Credenciales inválidas");

            var token = GenerateJwt(user);
            return (true, token);
        }

        // ============================
        // ✅ GENERAR JWT
        // ============================
        private string GenerateJwt(Usuario user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Correo),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString()),
                new Claim(ClaimTypes.Name, $"{user.Nombre} {user.Apellido}"),
                new Claim(ClaimTypes.Role, user.IdRol.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _cfg["Jwt:Issuer"],
                audience: _cfg["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_cfg["Jwt:ExpireMinutes"] ?? "60")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
