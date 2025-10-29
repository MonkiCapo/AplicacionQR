using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppQR.Core.Entidades;
using AppQR.Core.Dto;
using AppQR.Core.Servicios.Enums;
using AppQR.Core.Servicios.Repositorios;

namespace AppQR.WebAPI.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarifaController : ControllerBase
    {
        private readonly ITarifaRepositorio _tarifaRepo;
        private readonly IFuncionRepositorio _funcionRepo;

        public TarifaController(ITarifaRepositorio tarifaRepo, IFuncionRepositorio funcionRepo)
        {
            _tarifaRepo = tarifaRepo;
            _funcionRepo = funcionRepo;
        }

        [HttpPost]
        public IActionResult CrearTarifa([FromBody] TarifaDTO dto)
        {
            var funcion = _funcionRepo.ObtenerPorID(dto.IdFuncion);
            if (funcion == null)
                return BadRequest("La función seleccionada no existe");

            var tarifa = new Tarifa
            {
                Tipo = Enum.TryParse<ETipoTarifa>(dto.Tipo, true, out var tipo) ? tipo : ETipoTarifa.General,
                Precio = dto.Precio,
                Stock = dto.Stock,
                Estado = EEstados.Activo,
                funcion = funcion
            };

            var TarifaCreada = _tarifaRepo.AgregarTarifa(tarifa);
            return CreatedAtAction(nameof(ObtenerTarifaPorId), new { tarifaId = TarifaCreada.IdTarifa }, TarifaCreada);
        }

        [HttpGet("{tarifaId}")]
        public IActionResult ObtenerTarifaPorId(int tarifaId)
        {
            var tarifa = _tarifaRepo.ObtenerTarifaPorID(tarifaId);
            if (tarifa == null)
                return NotFound($"No se encontró la tarifa con ID {tarifaId}.");

            return Ok(tarifa);
        }

        [HttpGet("/funciones/{funcionId}/tarifas")]
        public IActionResult ObtenerTarifasPorFuncion(int funcionId)
        {
            var tarifas = _tarifaRepo.ObtenerTarifasPorFuncion(funcionId);
            if (!tarifas.Any())
                return NotFound($"No se encontraron tarifas para la función con ID {funcionId}.");

            return Ok(tarifas);
        }
    }
}