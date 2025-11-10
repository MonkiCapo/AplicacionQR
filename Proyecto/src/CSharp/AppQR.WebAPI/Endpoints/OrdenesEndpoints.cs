using AppQR.Core.Servicios.IServicios;
using AppQR.Core.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace AppQR.WebAPI.Endpoints
{
    public static class OrdenesEndpoints
    {
        public static void MapOrdenEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/Ordenes", (IOrdenService service) =>
            {
                var ordenes = service.ObtenerOrdenes();
                return Results.Ok(ordenes);
            }).WithTags("Orden");

            app.MapGet("/api/Ordenes/{id}", (int id, IOrdenService service) =>
            {
                var orden = service.ObtenerOrdenPorID(id);
                return orden is not null ? Results.Ok(orden) : Results.NotFound();
            }).WithTags("Orden");

            app.MapPost("/api/Ordenes", (OrdenDTO dto, IOrdenService service) =>
            {
                service.AgregarOrden(dto);
                return Results.Created();
            }).WithTags("Orden");

            app.MapPost("/api/Ordenes/{id}/pagar", (int id, EntradaDTO dto, IOrdenService service) =>
            {
                var resultado = service.PagarOrden(id, dto);
                return Results.Ok(new { Mensaje = resultado });
            }).WithTags("Orden");

            app.MapPost("/api/Ordenes/{id}/cancelar", (int id, IOrdenService service) =>
            {
                var resultado = service.CancelarOrden(id);
                return Results.Ok(new { Mensaje = resultado });
            }).WithTags("Orden");
        }
    }
}