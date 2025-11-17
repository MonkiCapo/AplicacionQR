using AppQR.Core.Servicios.IServicios;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace AppQR.Services.Servicios
{
    public class UrlConstructService : IUrlConstructService
    {
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly LinkGenerator _linkGenerator;

        public UrlConstructService(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator)
        {
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
        }

        public string GenerarUrldeQR(string token)
        {
            var httpContext = _httpContextAccessor.HttpContext!;
            var routeValues = new RouteValueDictionary { ["codigo"] = token };
            var qrUrl = _linkGenerator.GetUriByAddress(httpContext, "ValidarQr", routeValues);
            return qrUrl!;
        }
    }
}