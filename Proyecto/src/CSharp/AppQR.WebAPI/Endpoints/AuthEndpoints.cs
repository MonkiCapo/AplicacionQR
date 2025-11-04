using AppQR.Core.Servicios.IServicios;
using AppQR.Core.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using AppQR.Services.Servicios;

namespace AppQR.WebAPI.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("api/Auth/register", (RegisterRequestDTO dto, AuthService service) =>
            {
                service.RegistrarUsuario(dto);
                return Results.Ok();
            }).WithTags("Auth-Usuarios");

            app.MapPost("api/Auth/login", (LoginRequestDTO dto, AuthService service) =>
            {
                var result = service.LoginUsuario(dto);
                return Results.Ok(result);
            }).WithTags("Auth-Usuarios");
        }
    }
}