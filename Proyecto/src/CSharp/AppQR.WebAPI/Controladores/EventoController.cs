using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using AppQR.Core.Servicios;
using AppQR.Core.Entidades;
using AppQR.Core.Dto;
using AppQR.Core.Servicios.Enums;
using Microsoft.AspNetCore.Authorization;

namespace AppQR.WebAPI.Controladores
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IEventosRepositorio _eventoRepo;

        public EventoController(IEventosRepositorio eventoRepo)
        {
            _eventoRepo = eventoRepo;
        }

        [HttpPost]
        public IActionResult CrearEvento([FromBody] EventoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var evento = new Evento
            {
                Nombre = dto.Nombre,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                Estado = EEstados.Pendiente
            };

            var id = _eventoRepo.AgregarEvento(evento);
            return CreatedAtAction(nameof(ObtenerEventoPorId), new { id }, evento);
        }

        [HttpGet]
        public IActionResult ObtenerEventos()
        {
            var eventos = _eventoRepo.ObtenerEventos();
            return Ok(eventos);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerEventoPorId(int id)
        {
            var evento = _eventoRepo.ObtenerEventoPorID(id);
            if (evento == null)
                return NotFound($"No se encontró el evento con ID {id}");
            return Ok(evento);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarEvento(int id, [FromBody] EventoDTO dto)
        {
            var existente = _eventoRepo.ObtenerEventoPorID(id);
            if (existente == null)
                return NotFound($"No se encontró el evento con ID {id}");

            existente.Nombre = dto.Nombre;
            existente.FechaInicio = dto.FechaInicio;
            existente.FechaFin = dto.FechaFin;

            var actualizado = _eventoRepo.ActualizarEvento(existente);
            if (!actualizado)
                return StatusCode(500, "No se pudo actualizar el evento");

            return Ok(existente);
        }
        
        [HttpPost("{id}/publicar")]
        public IActionResult PublicarEvento(int id)
        {
            try
            {
                var mensaje = _eventoRepo.PublicarEvento(id);
                return Ok(new { mensaje });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("{id}/cancelar")]
        public IActionResult CancelarEvento(int id)
        {
            try
            {
                var mensaje = _eventoRepo.CancelarEvento(id);
                if (string.IsNullOrEmpty(mensaje))
                    return Ok(new { mensaje = "El evento fue cancelado correctamente" });
                return BadRequest(new { error = mensaje });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}