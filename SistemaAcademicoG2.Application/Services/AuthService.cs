using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SistemaAcademicoG2.Domain.Entities;
using SistemaAcademicoG2.Domain.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaAcademicoG2.Application.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository _repo;
        private readonly IConfiguration _config;

        public AuthService(IUsuarioRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        // ======================================================
        // ✅ REGISTRO
        // ======================================================
        public async Task<(bool ok, string msg)> RegisterAsync(
            string nombre,
            string apellido,
            string correo,
            string password,
            int idRol)
        {
            if (await _repo.ExisteCorreoAsync(correo))
                return (false, "El correo ya está registrado.");

            if (string.IsNullOrEmpty(password))
                return (false, "El password no puede ser vacío.");

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var nuevoUsuario = new Usuario
            {
                Nombre = nombre,
                Apellido = apellido,
                Correo = correo,
                PasswordHash = passwordHash,
                IdRol = idRol,
                Estado = true
            };

            await _repo.AddAsync(nuevoUsuario);

            return (true, "Usuario registrado correctamente.");
        }

        // ======================================================
        // ✅ LOGIN
        // ======================================================
        public async Task<(bool ok, string tokenOrMsg)> LoginAsync(string correo, string password)
        {
            Usuario? usuario = await _repo.GetByCorreoAsync(correo);

            if (usuario == null)
                return (false, "Credenciales incorrectas.");

            // Validar contraseña
            if (!BCrypt.Net.BCrypt.Verify(password, usuario.PasswordHash))
                return (false, "Credenciales incorrectas.");

            string token = GenerarToken(usuario);

            return (true, token);
        }

        // ======================================================
        // ✅ GENERAR TOKEN JWT
        // ======================================================
        private string GenerarToken(Usuario usuario)
        {
            // Validar existencia de clave JWT
            string keyString = _config["Jwt:Key"] ?? throw new Exception("Falta Jwt:Key en appsettings.json");
            string issuer = _config["Jwt:Issuer"] ?? throw new Exception("Falta Jwt:Issuer en appsettings.json");
            string audience = _config["Jwt:Audience"] ?? throw new Exception("Falta Jwt:Audience en appsettings.json");

            // Leer tiempo de expiración (en minutos)
            int expireMinutes = 60;
            if (!int.TryParse(_config["Jwt:ExpireMinutes"], out expireMinutes))
                expireMinutes = 60; // valor por defecto

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Correo),
                new Claim("nombre", usuario.Nombre),
                new Claim("apellido", usuario.Apellido),
                new Claim("rol", usuario.IdRol.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
