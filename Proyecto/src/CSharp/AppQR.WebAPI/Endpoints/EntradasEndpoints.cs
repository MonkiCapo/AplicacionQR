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
            app.MapGet("/api/entradas", (IEntradaService service) =>
            {
                var entradas = service.ObtenerEntradas();
                return Results.Ok(entradas);
            }).WithTags("Entrada");

            app.MapGet("/api/entradas/{id}", (int id, IEntradaService service) =>
            {
                var entrada = service.ObtenerEntradaPorID(id);
                return entrada is not null ? Results.Ok(entrada) : Results.NotFound();
            }).WithTags("Entrada");

            app.MapGet("/api/entradas/{id}/qr", (int id, IEntradaService service) =>
            {
                var resultado = service.ObtenerQR(id);
                return resultado is not null ? Results.File(resultado, "image/png") : Results.NotFound();
            }).WithTags("Entrada");

            app.MapPut("api/entradas/{id}/anular", (int id, IEntradaService service) =>
            {
                var resultado = service.AnularEntrada(id);
                return Results.Ok(new { Mensaje = resultado });
            }).WithTags("Entrada");
            
            app.MapPut("api/entradas/qr/validar", (int id, IEntradaService service) =>
            {
                var resultado = service.ValidarQR(id);
                return Results.Ok(resultado);
            }).WithTags("Entrada");
        }
    }
}