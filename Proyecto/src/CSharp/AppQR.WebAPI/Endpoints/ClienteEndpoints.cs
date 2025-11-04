using AppQR.Core.Servicios.IServicios;
using AppQR.Core.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace AppQR.WebAPI.Endpoints
{
    public static class ClienteEndpoints
    {
        public static void MapClienteEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/Cliente", [Authorize] (IClienteService service) =>
            {
                var clientes = service.ObtenerClientes();
                return Results.Ok(clientes);
            }).WithTags("Cliente");

            app.MapGet("/api/Cliente/{dni}", [Authorize] (int dni, IClienteService service) =>
            {
                var clientes = service.ObtenerClientePorDNI(dni);
                return clientes is not null ? Results.Ok(clientes) : Results.NotFound();
            }).WithTags("Cliente");

            app.MapPost("/api/Cliente", [Authorize] (ClienteDTO dto, IClienteService service) =>
            {
                service.AgregarCliente(dto);
                return Results.Created();
            }).WithTags("Cliente");

            app.MapPut("/api/Cliente/{dni}", [Authorize] (int dni, IClienteService service, ClienteActualizadoDTO dto) =>
            {
                service.ActualizarCliente(dto, dni);
                return Results.Ok();
            }).WithTags("Cliente");
        }
    }
}