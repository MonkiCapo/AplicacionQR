using AppQR.Core.Servicios.IServicios;
using AppQR.Core.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using AppQR.Services.Servicios;
using AppQR.Core.Servicios.Enums;

namespace AppQR.WebAPI.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("api/auth/register", (RegisterRequestDTO dto, AuthService service) =>
            {
                var resultado = service.RegistrarUsuario(dto);
                return Results.Ok(resultado);
            }).WithTags("Auth-Usuarios");

            app.MapPost("api/auth/login", (LoginRequestDTO dto, AuthService service) =>
            {
                var resultado = service.LoginUsuario(dto);
                return Results.Ok(resultado);
            }).WithTags("Auth-Usuarios");

            app.MapPost("api/auth/refresh", (RefreshTokenDTO dto, AuthService service) =>
            {
                var resultado = service.RefreshTokens(dto);
                return Results.Ok(resultado);
            }).WithTags("Auth-Usuarios");

            app.MapPost("api/auth/logout", (RefreshTokenDTO dto, AuthService service) =>
            {
                var resultado = service.Logout(dto);
                return Results.Ok(resultado);
            }).WithTags("Auth-Usuarios");

            app.MapGet("api/auth/me", (AuthService service) =>
            {
                var perfil = service.Me();
                return Results.Ok(perfil);
            }).WithTags("Auth-Usuarios");

            app.MapGet("api/auth/roles", () =>
            {
                var roles = Enum.GetNames(typeof(ERoles));
                return Results.Ok(roles);
            }).WithTags("Auth-Usuarios");

            app.MapPost("/api/usuarios/{id}/roles", (int id, string rol, AuthService service) =>
            {
                var resultado = service.AsignarRol(id, rol);
                return Results.Ok(resultado);
            }).WithTags("Auth-Usuarios");
        }
    }
}