using System;
using AppQR.Core.Dto;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios.Enums;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Servicios.Utilidades;

namespace AppQR.Services.Servicios
{
    public class AuthService
    {
        readonly IUsuarioRepositorio _usuarioRepo;
        readonly IRefreshTokenRepositorio _refreshTokenRepo;
        readonly RefreshTokenService _refreshTokenService;
        readonly IClienteRepositorio _clienteRepo;

        public AuthService(IUsuarioRepositorio usuarioRepo, IRefreshTokenRepositorio refreshTokenRepo, RefreshTokenService refreshTokenService, IClienteRepositorio clienteRepo)
        {
            _usuarioRepo = usuarioRepo;
            _refreshTokenRepo = refreshTokenRepo;
            _refreshTokenService = refreshTokenService;
            _clienteRepo = clienteRepo;
        }

        public Usuario RegistrarUsuario(RegisterRequestDTO nuevoUsuarioDTO)
        {
            if (_usuarioRepo.ExisteUsuario(nuevoUsuarioDTO.Email))
                throw new Exception("El email ya está en uso.");

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
            return usuario;
        }

        public object LoginUsuario(LoginRequestDTO loginDTO)
        {
            var usuario = _usuarioRepo.ObtenerUsuarioPorEmail(loginDTO.Email);
            if (usuario == null || !ContraseñaHasher.Verificar(usuario.Contraseña, loginDTO.Contraseña))
                throw new Exception("Credenciales inválidas.");

            var token = _refreshTokenService.GenerarToken(usuario);
            var refreshToken = Guid.NewGuid().ToString();

            var EntidadRefreshToken = new RefreshToken
            {
                Token = refreshToken,
                Email = usuario.Email,
                Expiration = DateTime.UtcNow.AddMinutes(30)
            };

            _refreshTokenRepo.InsertarToken(EntidadRefreshToken);

            return new
            {
                token,
                refreshToken,
                usuario = new
                {
                    usuario.IdUsuario,
                    usuario.NombreUsuario,
                    usuario.Email,
                    usuario.Rol
                }
            };
        }

        public object RefreshToken(RefreshTokenDTO refreshToken)
        {
            
            var tokenExistente = _refreshTokenRepo.ObtenerToken(refreshToken.RefreshToken);
            if (tokenExistente == null || tokenExistente.Expiration < DateTime.UtcNow)
                throw new Exception("Token inválido o expirado.");

             var usuario = _usuarioRepo.ObtenerUsuarioPorEmail(tokenExistente.Email);
            if (usuario == null)
                throw new Exception("Usuario no encontrado.");

            var nuevosTokens = _refreshTokenService.GenerarTokens(usuario);

            _refreshTokenRepo.ReemplazarToken(usuario.IdUsuario, nuevosTokens.RefreshToken, DateTime.UtcNow.AddMinutes(30));

            return nuevosTokens;
        }
    }
}