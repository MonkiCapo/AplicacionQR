using AppQR.Core.Servicios.IServicios;
using AppQR.Core.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace AppQR.WebAPI.Endpoints
{
    public static class TarifasEndpoints
    {
        public static void MapTarifaEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/Tarifa", (ITarifaService service) =>
            {
                var tarifas = service.ObtenerTodasLasTarifas();
                return Results.Ok(tarifas);
            }).WithTags("Tarifa");

            app.MapGet("/api/Tarifa/{id}", (int id, ITarifaService service) =>
            {
                var tarifa = service.ObtenerTarifaPorID(id);
                return tarifa is not null ? Results.Ok(tarifa) : Results.NotFound();
            }).WithTags("Tarifa");

            app.MapGet("/api/funciones/{idFuncion}/Tarifa", (int idFuncion, ITarifaService service) =>
            {
                var tarifas = service.ObtenerTarifasPorFuncion(idFuncion);
                return Results.Ok(tarifas);
            }).WithTags("Tarifa");

            app.MapPost("/api/Tarifa", (TarifaDTO dto, ITarifaService service) =>
            {
                service.AgregarTarifa(dto);
                return Results.Created();
            }).WithTags("Tarifa");

            app.MapDelete("/api/tarifas/{idTarifa}", (int id, ITarifaService service) =>
            {
                var tarifas = service.EliminarTarifa(id);
                return Results.Ok();
            }).WithTags("Tarifa");
        }
    }
}