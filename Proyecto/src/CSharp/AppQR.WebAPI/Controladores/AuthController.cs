using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios;

namespace AppQR.WebAPI.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly AuthService _authService;

        public AuthController(IUsuarioRepositorio usuarioRepo, AuthService authService)
        {
            _usuarioRepositorio = usuarioRepo;
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest req)
        {
            var existe = _usuarioRepo.ObtenerUsuarioPorEmail(req.Email);
            if (existe != null) return Conflict("Email ya registrado");

            var usuario = new Usuario
            {
                NombreUsuario = req.NombreUsuario,
                Email = req.Email,
                Contraseña = BCrypt.Net.BCrypt.HashPassword(req.Contraseña),
                Rol = req.Rol,
                cliente = new Cliente { DNI = req.DNI }
            };
            _usuarioRepo.AgregarUsuario(usuario);

            return Ok(new { mensaje = "Usuario registrado" });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest req)
        {
            var usuario = _usuarioRepo.ObtenerUsuarioPorEmail(req.Email);
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(req.Contraseña, usuario.Contraseña))
                return Unauthorized("Credenciales inválidas");

            var token = _authService.GenerarToken(usuario);

            return Ok(new { token });
        }
        
        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            var nombre = User.Identity.Name;
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;
            var email = User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;

            return Ok(new { nombre, email, rol });
        }
    }
}