using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Dto;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Servicios.Validadores;
using FluentValidation;
using AppQR.Core.Servicios.Utilidades;
using AppQR.Core.Servicios.Enums;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace AppQR.Services.Servicios
{
    public class AuthService
    {
        readonly IUsuarioRepositorio _usuarioRepo;
        readonly IRefreshTokenRepositorio _refreshTokenRepo;
        readonly RefreshTokenService _refreshTokenService;
        readonly IClienteRepositorio _clienteRepo;
        readonly UsuarioFluent _usuarioValidador;
        readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUsuarioRepositorio usuarioRepo, IRefreshTokenRepositorio refreshTokenRepo, RefreshTokenService refreshTokenService, IClienteRepositorio clienteRepo, UsuarioFluent usuarioFluent, IHttpContextAccessor httpContextAccessor)
        {
            _usuarioRepo = usuarioRepo;
            _refreshTokenRepo = refreshTokenRepo;
            _refreshTokenService = refreshTokenService;
            _clienteRepo = clienteRepo;
            _usuarioValidador = usuarioFluent;
            _httpContextAccessor = httpContextAccessor;
        }

        public object RegistrarUsuario(RegisterRequestDTO nuevoUsuarioDTO)
        {
            ((IValidator<RegisterRequestDTO>)_usuarioValidador).ValidateAndThrow(nuevoUsuarioDTO);

            if (_usuarioRepo.ExisteUsuario(nuevoUsuarioDTO.Email))
                throw new Exception("El email ya esta en uso");

            if (_clienteRepo.ExisteDNIdeCliente(nuevoUsuarioDTO.cliente.DNI))
            {
                var nuevoCliente = new Cliente
                {
                    Nombre = nuevoUsuarioDTO.cliente.Nombre,
                    DNI = nuevoUsuarioDTO.cliente.DNI,
                    Telefono = nuevoUsuarioDTO.cliente.Telefono
                };
                _clienteRepo.AgregarCliente(nuevoCliente);
            }

            var hash = ContraseñaHasher.Hash(nuevoUsuarioDTO.Contraseña);
            nuevoUsuarioDTO.Contraseña = hash;

            var usuario = new Usuario
            {
                NombreUsuario = nuevoUsuarioDTO.NombreUsuario,
                Contraseña = nuevoUsuarioDTO.Contraseña,
                Email = nuevoUsuarioDTO.Email,
                Rol = Enum.TryParse<ERoles>(nuevoUsuarioDTO.Rol, true, out var rol) ? rol : ERoles.Usuario,
                cliente = new Cliente
                {
                    DNI = nuevoUsuarioDTO.cliente.DNI,
                    Nombre = nuevoUsuarioDTO.cliente.Nombre,
                    Telefono = nuevoUsuarioDTO.cliente.Telefono
                }
            };

            _usuarioRepo.AgregarUsuario(usuario);

            return new
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
            };
        }

        public object LoginUsuario(LoginRequestDTO loginDTO)
        {
             ((IValidator<LoginRequestDTO>)_usuarioValidador).ValidateAndThrow(loginDTO);

            var usuario = _usuarioRepo.ObtenerUsuarioPorEmail(loginDTO.Email);

            if (usuario == null || !ContraseñaHasher.Verificar(usuario.Contraseña, loginDTO.Contraseña))
                throw new Exception("Credenciales inválidas.");

            var tokens = _refreshTokenService.GenerarTokens(usuario);

            var refreshTokenEntidad = new RefreshToken
            {
                Token = tokens.RefreshToken,
                Email = usuario.Email,
                Expiration = DateTime.UtcNow.AddMinutes(30)
            };

            _refreshTokenRepo.InsertarToken(refreshTokenEntidad);

            return new
            {
                tokens.Token,
                tokens.RefreshToken,
                usuario = new
                {
                    usuario.IdUsuario,
                    usuario.NombreUsuario,
                    usuario.Email,
                    usuario.Rol
                }
            };
        }

        public object RefreshTokens(RefreshTokenDTO refreshDTO)
        {
            var TokenExistente = _refreshTokenRepo.ObtenerToken(refreshDTO.RefreshToken);
            if (TokenExistente == null || TokenExistente.Expiration < DateTime.Now)
                throw new Exception("EL refresh de este token no se puede realizar porque es invalido o esta expirado");

            var usuario = _usuarioRepo.ObtenerUsuarioPorEmail(TokenExistente.Email);
            if (usuario == null)
                throw new Exception("Usuario no encontrado");

            var nuevosTokens = _refreshTokenService.GenerarTokens(usuario);

            _refreshTokenRepo.ReemplazarToken(usuario.IdUsuario, nuevosTokens.RefreshToken, DateTime.UtcNow.AddMinutes(30));

            return nuevosTokens;
        }

        public object AsignarRol(int idUsuario, string rol)
        {
            var usuarioActual = _httpContextAccessor.HttpContext?.User;

            if (usuarioActual == null || !usuarioActual.IsInRole("Admin"))
                throw new Exception("Solo usuarios con el rol Admin pueden cambiar los roles");

            var usuario = _usuarioRepo.ObtenerUsuarioPorID(idUsuario);
            if (usuario == null)
                throw new Exception("Usuario no encontrado");

            if (!Enum.TryParse<ERoles>(rol, out var nuevoRol))
                throw new Exception("");

            usuario.Rol = nuevoRol;
            _usuarioRepo.ActualizarRol(usuario.IdUsuario, nuevoRol.ToString());

            return usuario;
        }
    }
}