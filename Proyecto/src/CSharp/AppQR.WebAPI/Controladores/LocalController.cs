using Microsoft.AspNetCore.Mvc;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios;
using AppQR.Core.Dto;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace AppQR.WebAPI.Controladores
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class LocalController : ControllerBase
    {
        private readonly ILocalRepositorio _LocalRepo;
        public LocalController(ILocalRepositorio localRepo)
        {
            _LocalRepo = localRepo;
        }

        [HttpGet]
        public IActionResult ObtenerLocales()
        {
            var locales = _LocalRepo.ObtenerLocales();
            return Ok(locales);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerLocalPorID(int id)
        {
            var local = _LocalRepo.ObtenerLocalPorID(id);
            if (local == null)
            {
                return NotFound();
            }
            return Ok(local);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarLocal(int id, [FromBody] LocalDTO DTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var local = new Local
            {
                IdLocal = id,
                Nombre = DTO.Nombre,
                Direccion = DTO.Direccion
            };
            local.IdLocal = id;
            var ok = _LocalRepo.ActualizarLocal(local);
            return ok ? NoContent() : NotFound();
        }

        [HttpPost]
        public IActionResult CrearLocal([FromBody] LocalDTO DTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var nuevoLocal = new Local
            {
                Nombre = DTO.Nombre,
                Direccion = DTO.Direccion
            };
            var id = _LocalRepo.AgregarLocal(nuevoLocal);
            return CreatedAtAction(nameof(ObtenerLocalPorID), new { id }, nuevoLocal);
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarLocal(int id)
        {
            var ok = _LocalRepo.EliminarLocal(id);
            return ok ? NoContent() : NotFound();
        }

        [HttpGet("{id}/sectores")]
        public IActionResult ObtenerSectoresPorLocal(int id) =>
            Ok(_LocalRepo.ObtenerSectoresPorLocal(id));

        [HttpPost("{id}/sectores")]
        public IActionResult CrearSector(int id, [FromBody] SectorDTO DTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var local = _LocalRepo.ObtenerLocalPorID(id);

            var sector = new Sector
            {
                local = local,
                Capacidad = DTO.Capacidad
            };
            var nuevoSector = _LocalRepo.AgregarSector(sector, id);
            return CreatedAtAction(nameof(ObtenerSectoresPorLocal), new { id }, sector);
        }

        [HttpPut("sectores/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ActualizarSector(int id, [FromBody] SectorDTO Dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sector = _LocalRepo.ObtenerSectorPorID(id);
            if (sector == null) return NotFound("Este sector no fue encontrado");

            sector.Capacidad = Dto.Capacidad;

            var ok = _LocalRepo.ActualizarSector(sector);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("sectores/{IDSECTOR}")]
        public IActionResult EliminarUnSector(int IDSECTOR) =>
            _LocalRepo.EliminarSector(IDSECTOR) ? NoContent() : NotFound();
    }
}