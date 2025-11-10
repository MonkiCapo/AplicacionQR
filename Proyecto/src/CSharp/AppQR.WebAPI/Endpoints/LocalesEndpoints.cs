using AppQR.Core.Servicios.IServicios;
using AppQR.Core.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace AppQR.WebAPI.Endpoints
{
    public static class LocalesEndpoints
    {
        public static void MapLocalEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/Local", (ILocalService service) =>
            {
                var locales = service.ObtenerLocales();
                return Results.Ok(locales);
            }).WithTags("Local");

            app.MapGet("/api/Local/{id}", (int id, ILocalService service) =>
            {
                var local = service.ObtenerLocalPorID(id);
                return local is not null ? Results.Ok(local) : Results.NotFound();
            }).WithTags("Local");

            app.MapPost("/api/Local", (LocalDTO dto, ILocalService service) =>
            {
                service.AgregarLocal(dto);
                return Results.Created();
            }).WithTags("Local");

            app.MapPut("/api/Local/{id}", (int id, LocalDTO dto, ILocalService service) =>
            {
                service.ActualizarLocal(dto, id);
                return Results.Ok();
            }).WithTags("Local");

            app.MapDelete("/api/Local/{id}", (int id, ILocalService service) =>
            {
                service.EliminarLocal(id);
                return Results.Ok();
            }).WithTags("Local");
        }
    }
}