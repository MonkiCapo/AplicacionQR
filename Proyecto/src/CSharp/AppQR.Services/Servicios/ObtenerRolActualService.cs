using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using AppQR.Core.Servicios.IServicios;

namespace AppQR.Services.Servicios
{
    public class ObtenerRolActualService : IObtenerRolActualService
    {
        readonly IHttpContextAccessor _httpContext;

        public ObtenerRolActualService(IHttpContextAccessor httpContext) => _httpContext = httpContext;

        public string GetRolActual()
        {
            var User = _httpContext.HttpContext?.User;
            var rol = User.FindFirst(ClaimTypes.Role)?.Value;
            if (rol is null) return "Default";
            return rol;
        }
    }
}