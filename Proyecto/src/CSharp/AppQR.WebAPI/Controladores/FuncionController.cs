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
    [Route("api/[controller]")]
    public class FuncionController : ControllerBase
    {
        private readonly IFuncionRepositorio _funcionrepo;
        private readonly IEventosRepositorio _eventorepo;

        public FuncionController(IFuncionRepositorio funcionrepo, IEventosRepositorio eventorepo)
        {
            _funcionrepo = funcionrepo;
            _eventorepo = eventorepo;
        }

        [HttpGet]
        public IActionResult ObtenerFunciones()
        {
            var funciones = _funcionrepo.ObtenerTodasLasFunciones();
            return Ok(funciones);
        }

        [HttpPost]
        public IActionResult CrearFuncion([FromBody] FuncionDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var evento = _eventoRepo.ObtenerPorID(dto.idEvento);
            if (evento == null)
                return NotFound($"El evento con ID {dto.idEvento} no existe.");

            var funcion = new Funcion
            {
                Nombre = dto.Nombre,
                FechaHora = dto.FechaHora,
                Estado = Enum.TryParse<EEstados>(dto.Estado, true, out var estado) ? estado : EEstados.Creado,
                evento = evento
            };

            var nuevaFuncion = _funcionRepo.AgregarFuncion(funcion);
            return CreatedAtAction(nameof(ObtenerFuncionPorId), new { id = nuevaFuncion.IdFuncion }, nuevaFuncion);
        }

        [HttpGet("{id}")]
        public IActionResult ObtenerFuncionPorId(int id)
        {
            var funcion = _funcionrepo.ObtenerPorID(id);
            if (funcion == null)
                return NotFound($"No se encontró la función con ID {id}.");
            return Ok(funcion);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarFuncion(int id, [FromBody] FuncionDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var funcionExistente = _funcionRepo.ObtenerPorID(id);
            if (funcionExistente == null)
                return NotFound($"No se encontró la función con ID {id}.");

            var evento = _eventoRepo.ObtenerPorID(dto.idEvento);
            if (evento == null)
                return NotFound($"El evento con ID {dto.idEvento} no existe.");

            funcionExistente.Nombre = dto.Nombre;
            funcionExistente.FechaHora = dto.FechaHora;
            funcionExistente.Estado = Enum.TryParse<EEstados>(dto.Estado, true, out var estado) ? estado : funcionExistente.Estado;
            funcionExistente.evento = evento;

            var actualizado = _funcionRepo.ActualizarFuncion(funcionExistente);
            if (!actualizado)
                return StatusCode(500, "No se pudo actualizar la función.");

            return Ok("Función actualizada correctamente.");
        }

        [HttpPost("{id}/cancelar")]
        public IActionResult CancelarFuncion(int id)
        {
            var resultado = _funcionrepo.CancelarFuncion(id);
            return ok(resultado);
        }
    }
}