using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppQR.Core;
using AppQR.Core.Servicios;
using AppQR.Core.Entidades;
using AppQR.Core.Dto;
using AppQR.Core.Servicios.Enums;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AppQR.Core.Servicios.Utilidades;
using AppQR.Dapper;

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

        public IActionResult Login([FromServices] RefreshTokenRepositorio refreshTokenRepo, [FromBody] LoginRequestDTO login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hash = ContraseñaHasher.Hash(login.Contraseña);
            login.Contraseña = hash;

            var usuario = _usuarioRepo.Login(login.Email, login.Contraseña);
            if (usuario == null)
                return Unauthorized("Credenciales inválidas.");

            var token = GenerateJwtToken(usuario);
            var refreshToken = Guid.NewGuid().ToString();
            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                Email = usuario.Email,
                Expiration = DateTime.UtcNow.AddMinutes(30)
            };
            refreshTokenRepo.InsertarToken(refreshTokenEntity);

            return Ok(new { token, refreshToken });
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
        }
    }
}