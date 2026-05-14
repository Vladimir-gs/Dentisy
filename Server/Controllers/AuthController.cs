using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Server.Context;
using Shared.DTOs;
using Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DentisyContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(DentisyContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/Auth/login
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDTO>> Login(LoginDTO loginDto)
        {
            // Buscar usuario por correo, incluyendo su rol
            var usuario = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(u => u.Correo == loginDto.Correo);

            if (usuario == null)
                return Unauthorized(new { message = "Correo o contraseña incorrectos." });

            // Verificar que el usuario esté activo
            if (usuario.Estado == false)
                return Unauthorized(new { message = "El usuario se encuentra inactivo." });

            // Verificar contraseña con BCrypt
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, usuario.PasswordHash))
                return Unauthorized(new { message = "Correo o contraseña incorrectos." });

            // Generar JWT
            var token = GenerateJwtToken(usuario);

            return Ok(new AuthResponseDTO
            {
                Token = token,
                Nombre = $"{usuario.Nombre} {usuario.Apellido}",
                Correo = usuario.Correo,
                Rol = usuario.IdRolNavigation?.Nombre ?? "Sin Rol"
            });
        }

        // POST: api/Auth/register
        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDTO>> Register(RegisterDTO registerDto)
        {
            // Verificar si el correo ya existe
            if (await _context.Usuarios.AnyAsync(u => u.Correo == registerDto.Correo))
                return Conflict(new { message = "Ya existe un usuario con este correo." });

            // Verificar que el rol exista
            var rol = await _context.Roles.FindAsync(registerDto.IdRol);
            if (rol == null)
                return BadRequest(new { message = "El rol especificado no existe." });

            // Crear el usuario con password hasheado
            var usuario = new Usuarios
            {
                Nombre = registerDto.Nombre,
                Apellido = registerDto.Apellido,
                Correo = registerDto.Correo,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Telefono = registerDto.Telefono,
                IdRol = registerDto.IdRol,
                Estado = true,
                FechaCreacion = DateTime.Now
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            // Cargar la navegación del rol para la respuesta
            usuario.IdRolNavigation = rol;

            // Generar JWT
            var token = GenerateJwtToken(usuario);

            return CreatedAtAction(nameof(Login), new AuthResponseDTO
            {
                Token = token,
                Nombre = $"{usuario.Nombre} {usuario.Apellido}",
                Correo = usuario.Correo,
                Rol = rol.Nombre
            });
        }

        private string GenerateJwtToken(Usuarios usuario)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Name, $"{usuario.Nombre} {usuario.Apellido}"),
                new Claim(ClaimTypes.Role, usuario.IdRolNavigation?.Nombre ?? "Sin Rol")
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    double.Parse(jwtSettings["ExpireMinutes"] ?? "60")),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
