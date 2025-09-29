using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AppQR.Core.Servicios
{
    public class AuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerarToken(Usuario usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.IdUsuario.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, usuario.NombreUsuario),
                new Claim(ClaimTypes.Role, usuario.Rol),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}