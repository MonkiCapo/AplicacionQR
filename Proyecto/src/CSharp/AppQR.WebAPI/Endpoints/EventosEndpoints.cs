using AppQR.Core.Servicios.IServicios;
using AppQR.Core.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace AppQR.WebAPI.Endpoints
{
    public static class EventosEndpoints
    {
        public static void MapEventoEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/Evento", (IEventoService service) =>
            {
                var eventos = service.ObtenerEventos();
                return Results.Ok(eventos);
            }).WithTags("Evento").RequireAuthorization("Cliente");

            app.MapGet("/api/Evento/{id}", (int id, IEventoService service) =>
            {
                var eventos = service.ObtenerEventoPorID(id);
                return eventos is not null ? Results.Ok(eventos) : Results.NotFound();
            }).WithTags("Evento").RequireAuthorization("Cliente");

            app.MapPost("/api/Evento", (EventoDTO dto, IEventoService service) =>
            {
                service.AgregarEvento(dto);
                return Results.Created();
            }).WithTags("Evento").RequireAuthorization("Organizador");

            app.MapPut("/api/Evento/{id}", (int id, IEventoService service, EventoDTO dto) =>
            {
                service.ActualizarEvento(dto, id);
                return Results.Ok();
            }).WithTags("Evento").RequireAuthorization("Organizador");

            app.MapPut("/api/Evento/{id}/publicar", (int id, IEventoService service) =>
            {
                service.PublicarEvento(id);
                return Results.Ok();
            }).WithTags("Evento").RequireAuthorization("Organizador");

            app.MapPut("/api/Evento/{id}/cancelar", (int id, IEventoService service) =>
            {
                service.CancelarEvento(id);
                return Results.Ok();
            }).WithTags("Evento").RequireAuthorization("Organizador");
        }
    }
}