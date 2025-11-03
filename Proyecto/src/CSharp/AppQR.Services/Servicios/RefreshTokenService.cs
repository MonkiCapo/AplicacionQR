using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using AppQR.Core.Entidades;
using AppQR.Core.Dto;

namespace AppQR.Services.Servicios
{
    public class RefreshTokenService
    {
        private readonly IConfiguration _configuration;

        public RefreshTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerarToken(Usuario usuario)
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

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: null,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerarRefreshToken() => Guid.NewGuid().ToString();

        public RefreshTokenDTO GenerarTokens(Usuario usuario)
        {
            var Token = GenerarToken(usuario);
            var RefreshToken = GenerarRefreshToken();

            return new RefreshTokenDTO
            {
                Token = Token,
                RefreshToken = RefreshToken
            };
        }
    }
}