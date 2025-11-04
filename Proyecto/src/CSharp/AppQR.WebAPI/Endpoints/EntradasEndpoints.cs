using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Servicios.IServicios;

namespace AppQR.WebAPI.Endpoints
{
    public static class EntradasEndpoints
    {
        public static void MapEntradaEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/Entradas", (IEntradaService service) =>
            {
                var entradas = service.ObtenerEntradas();
                return Results.Ok(entradas);
            }).WithTags("Entrada");

            app.MapGet("/api/Entradas/{id}", (int id, IEntradaService service) =>
            {
                var entrada = service.ObtenerEntradaPorID(id);
                return entrada is not null ? Results.Ok(entrada) : Results.NotFound();
            }).WithTags("Entrada");

            app.MapPost("api/Entradas/{id}/anular", (int id, IEntradaService service) =>
            {
                var resultado = service.AnularEntrada(id);
                return Results.Ok(new{ Mensaje = resultado });
            }).WithTags("Entrada");
        }
    }
}