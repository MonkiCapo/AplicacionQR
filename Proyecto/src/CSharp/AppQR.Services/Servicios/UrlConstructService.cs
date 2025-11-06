using AppQR.Core.Servicios.IServicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

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

        public string GenerarUrldeQR(int id)
        {
            var httpContext = _httpContextAccessor.HttpContext!;
            var routeValues = new RouteValueDictionary { ["id"] = id };
            var qrUrl = _linkGenerator.GetUriByPage(httpContext, "/ValidarQr", null, routeValues);
            return qrUrl!;
        }
    }
}