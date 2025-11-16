using AppQR.Core.Servicios.IServicios;
using AppQR.Core.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace AppQR.WebAPI.Endpoints
{
    public static class FuncionesEndpoints
    {
        public static void MapFuncionEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/Funcion", (IFuncionService service) =>
            {
                var funciones = service.ObtenerTodasLasFunciones();
                return Results.Ok(funciones);
            }).WithTags("Funcion").RequireAuthorization("Cliente");

            app.MapGet("/api/Funcion/{id}", (int id, IFuncionService service) =>
            {
                var funcion = service.ObtenerPorID(id);
                return funcion is not null ? Results.Ok(funcion) : Results.NotFound();
            }).WithTags("Funcion").RequireAuthorization("Cliente");

            app.MapPost("/api/Funcion", (FuncionDTO dto, IFuncionService service) =>
            {
                service.AgregarFuncion(dto);
                return Results.Created();
            }).WithTags("Funcion").RequireAuthorization("Organizador");

            app.MapPut("/api/Funcion/{id}", (int id, FuncionDTO dto, IFuncionService service) =>
            {
                service.ActualizarFuncion(dto, id);
                return Results.Ok();
            }).WithTags("Funcion").RequireAuthorization("Organizador");

            app.MapDelete("/api/Funcion/{id}", (int id, IFuncionService service) =>
            {
                service.EliminarFuncion(id);
                return Results.Ok();
            }).WithTags("Funcion").RequireAuthorization("Organizador");

            app.MapPut("/api/Funcion/{idFuncion}/Cancelar", (int idFuncion, IFuncionService service) =>
            {
                service.CancelarFuncion(idFuncion);
                return Results.Ok();
            }).WithTags("Funcion").RequireAuthorization("Organizador");
        }
    }
}