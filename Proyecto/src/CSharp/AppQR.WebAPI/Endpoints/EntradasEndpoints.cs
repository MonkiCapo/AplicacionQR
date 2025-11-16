using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Servicios.IServicios;
using Microsoft.AspNetCore.Mvc;

namespace AppQR.WebAPI.Endpoints
{
    public static class EntradasEndpoints
    {
        public static void MapEntradaEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/", () => "Hola mundo");

            app.MapGet("/api/entradas", (IEntradaService service) =>
            {
                var entradas = service.ObtenerEntradas();
                return Results.Ok(entradas);
            }).WithTags("Entrada").RequireAuthorization("Cliente");

            app.MapGet("/api/entradas/{id}", (int id, IEntradaService service) =>
            {
                var entrada = service.ObtenerEntradaPorID(id);
                return entrada is not null ? Results.Ok(entrada) : Results.NotFound();
            }).WithTags("Entrada").RequireAuthorization("Cliente");

            app.MapGet("/api/entradas/{id}/qr", (int id, IEntradaService service) =>
            {
                var resultado = service.ObtenerQR(id);
                return resultado is not null ? Results.File(resultado, "image/png") : Results.NotFound();
            }).WithTags("Entrada").RequireAuthorization("Cliente");

            app.MapPut("api/entradas/{id}/anular", (int id, IEntradaService service) =>
            {
                var resultado = service.AnularEntrada(id);
                return Results.Ok(new { Mensaje = resultado });
            }).WithTags("Entrada").RequireAuthorization("Cliente");

            app.MapGet("api/entradas/qr/validar", ([FromQuery] int id, IEntradaService service) =>
            {
                var resultado = service.ValidarQR(id);
                return Results.Ok(resultado);
            }).WithTags("Entrada").RequireAuthorization("Organizador").WithName("ValidarQr");
        }
    }
}