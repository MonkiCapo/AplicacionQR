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
            if (ModelState.IsValid)
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

            var hash = 
        }
    }
}