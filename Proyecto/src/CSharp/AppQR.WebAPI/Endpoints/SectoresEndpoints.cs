using AppQR.Core.Servicios.IServicios;
using AppQR.Core.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace AppQR.WebAPI.Endpoints
{
    public static class SectoresEndpoints
    {
        public static void MapSectorEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/Local/{idLocal}/Sector", (int idLocal, ILocalService service) =>
            {
                var sectores = service.ObtenerSectoresPorLocal(idLocal);
                return Results.Ok(sectores);
            }).WithTags("Sector");

            app.MapGet("/api/Sector/{id}", (int id, ILocalService service) =>
            {
                var sector = service.ObtenerSectorPorID(id);
            return sector is not null ? Results.Ok(sector) : Results.NotFound();
            }).WithTags("Sector");

            app.MapPost("/api/Local/{id}/Sector", (int id, SectorDTO dto, ILocalService service) =>
            {
                service.AgregarSector(dto, id);
                return Results.Created();
            }).WithTags("Sector");

            app.MapPut("api/Sector/{id}", (int id, SectorDTO dto, ILocalService service) =>
            {
                service.ActualizarSector(dto, id);
                return Results.Ok();
            }).WithTags("Sector");

            app.MapDelete("/api/Sector({id}", (int id, ILocalService service) =>
            {
                service.EliminarSector(id);
                return Results.Ok();
            }).WithTags("Sector");
        }
    }
}