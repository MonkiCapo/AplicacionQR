using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppQR.Core;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Entidades;
using AppQR.Core.Dto;
using AppQR.Core.Servicios.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AppQR.Core.Servicios.Utilidades;
using AppQR.Dapper;
using System.Text;

namespace AppQR.WebAPI.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepo;
        private readonly IClienteRepositorio _clienteRepo;
        private readonly IConfiguration _config;
        private readonly IRefreshTokenRepositorio _refreshTokenRepo;

        public AuthController(IUsuarioRepositorio usuarioRepo, IClienteRepositorio clienteRepo, IConfiguration config, IRefreshTokenRepositorio refreshTokenRepo)
        {
            _usuarioRepo = usuarioRepo;
            _clienteRepo = clienteRepo;
            _config = config;
            _refreshTokenRepo = refreshTokenRepo;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDTO nuevoUsuarioDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_usuarioRepo.ExisteUsuario(nuevoUsuarioDTO.Email))
                return BadRequest("El email ya está en uso.");

            if (!_clienteRepo.ExisteDNIdeCliente(nuevoUsuarioDTO.cliente.DNI))
            {
                var nuevoCliente = new Cliente
                {
                    Nombre = nuevoUsuarioDTO.cliente.Nombre,
                    DNI = nuevoUsuarioDTO.cliente.DNI,
                    Telefono = nuevoUsuarioDTO.cliente.Telefono
                };
                _clienteRepo.AgregarCliente(nuevoCliente);
                nuevoUsuarioDTO.cliente = new ClienteDTO
                {
                    Nombre = nuevoCliente.Nombre,
                    DNI = nuevoCliente.DNI,
                    Telefono = nuevoCliente.Telefono
                };
            }

            var hash = ContraseñaHasher.Hash(nuevoUsuarioDTO.Contraseña);
            nuevoUsuarioDTO.Contraseña = hash;

            var usuario = new Usuario
            {
                NombreUsuario = nuevoUsuarioDTO.NombreUsuario,
                Contraseña = nuevoUsuarioDTO.Contraseña,
                Email = nuevoUsuarioDTO.Email,
                Rol = ERoles.Usuario,
                cliente = new Cliente
                {
                    DNI = nuevoUsuarioDTO.cliente.DNI,
                    Nombre = nuevoUsuarioDTO.cliente.Nombre,
                    Telefono = nuevoUsuarioDTO.cliente.Telefono
                }
            };

            _usuarioRepo.AgregarUsuario(usuario);

            return Ok(new
            {
                Mensaje = "Usuario registrado correctamente",
                usuario = new
                {
                    usuario.IdUsuario,
                    usuario.NombreUsuario,
                    usuario.Email,
                    usuario.Rol,
                    Cliente = usuario.cliente
                }
            });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Buscar usuario solo por email
            var usuario = _usuarioRepo.ObtenerUsuarioPorEmail(login.Email);
            
            // Si el usuario no existe, login fallido
            if (usuario == null)
                return Unauthorized("Credenciales inválidas.");

            
            if (!ContraseñaHasher.Verificar(usuario.Contraseña, login.Contraseña))
                return Unauthorized("Credenciales inválidas.");
            
            // Generar token JWT
            var token = GenerateJwtToken(usuario);
            var refreshToken = Guid.NewGuid().ToString();
            
            // Guardar refresh token en la base de datos
            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                Email = usuario.Email,
                Expiration = DateTime.UtcNow.AddMinutes(30)
            };
            _refreshTokenRepo.InsertarToken(refreshTokenEntity);

            // Retornar éxito con tokens e información del usuario
            return Ok(new { 
                token, 
                refreshToken,
                usuario = new {
                    usuario.IdUsuario,
                    usuario.NombreUsuario,
                    usuario.Email,
                    usuario.Rol
                }
            });
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Email),
                new Claim(ClaimTypes.Role, string.IsNullOrEmpty(usuario.Rol.ToString()) ? "Usuario" : usuario.Rol.ToString()),
                new Claim("NombreUsuario", usuario.NombreUsuario),
                new Claim("DNI", usuario.cliente.DNI.ToString()),
                new Claim("Nombre", usuario.cliente.Nombre),
                new Claim("Telefono", usuario.cliente.Telefono ?? "")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshTokenDTO refreshRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var TokenExistente = _refreshTokenRepo.ObtenerToken(refreshRequest.RefreshToken);
            if (TokenExistente == null || TokenExistente.Expiration < DateTime.UtcNow)
                return Unauthorized("El refresh de este token es inválido o esta expirado.");

            var usuario = _usuarioRepo.ObtenerUsuarioPorEmail(TokenExistente.Email);
             if (usuario == null)
                return Unauthorized("Usuario no encontrado");

            var newToken = GenerateJwtToken(usuario);
            var newRefreshToken = Guid.NewGuid().ToString();
            var newRefreshTokenHash = ContraseñaHasher.Hash(newRefreshToken);

            _refreshTokenRepo.ReemplazarToken(usuario.IdUsuario, newRefreshTokenHash, DateTime.UtcNow.AddMinutes(30));

            return Ok(new { token = newToken, refreshToken = newRefreshToken });
        }

        [HttpPost("logout")]
        public IActionResult Logout([FromBody] RefreshTokenDTO refreshTokenDto)
        {
            _refreshTokenRepo.EliminarToken(refreshTokenDto.RefreshToken);
            return Ok(new { Mensaje = "Sesión cerrada correctamente." });
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            var email = User.Identity?.Name;
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;
            var nombreUsuario = User.FindFirst("NombreUsuario")?.Value;
            var dni = User.FindFirst("DNI")?.Value;
            var nombre = User.FindFirst("Nombre")?.Value;

            return Ok(new
            {
                Email = email,
                Rol = rol,
                NombreUsuario = nombreUsuario,
                DNI = dni,
                Nombre = nombre
            });
        }

        [HttpGet("roles")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetRoles()
        {
            var roles = new[] { ERoles.Admin, ERoles.Usuario };
            return Ok(roles);
        }

        [HttpPost("/api/usuarios/{idUsuario}/roles")]
        [Authorize(Roles = "Admin")]
        public IActionResult AsignarRol(int idUsuario, [FromBody] string rol)
        {
            var usuario = _usuarioRepo.ObtenerUsuarioPorID(idUsuario);
            if (usuario == null)
                return NotFound("Usuario no encontrado.");

            if (ERoles.Usuario.ToString().Trim() != rol.Trim() || ERoles.Admin.ToString().Trim() != rol.Trim())
                return BadRequest("Rol inválido.");

            if (ERoles.Usuario.ToString().Trim() == rol.Trim())
                usuario.Rol = ERoles.Usuario;
            else
            {
                usuario.Rol = ERoles.Admin;
            }

            _usuarioRepo.ActualizarUsuario(usuario);

            return Ok(usuario);
        }
    }
}