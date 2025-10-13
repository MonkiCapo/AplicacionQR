using Microsoft.AspNetCore.Mvc;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios;
using AppQR.Core.Dto;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using AppQR.Core.Servicios.Enums;

namespace AppQR.WebAPI.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenController : ControllerBase
    {
        private readonly IOrdenRepositorio _OrdenRepo;
        private readonly IEntradaRepositorio _EntradaRepo;
        private readonly IUsuarioRepositorio _UsuarioRepo;

        public OrdenController(IOrdenRepositorio ordenRepo, IEntradaRepositorio entradaRepo, IUsuarioRepositorio usuarioRepo)
        {
            _OrdenRepo = ordenRepo;
            _EntradaRepo = entradaRepo;
            _UsuarioRepo = usuarioRepo;
        }

        [HttpPost]
        public IActionResult CrearOrden([FromBody] OrdenDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("Se debe enviar un cuerpo");
            }

            var usuarioOrden = _UsuarioRepo.ObtenerUsuarioPorEmail(dto.Email);

            if (usuarioOrden == null)
            {
                return BadRequest($"El usuario con el email:'{dto.Email}' no se pudo encontrar");
            }

            var ordenHecha = new Orden
            {
                Estado = EEstados.Creado,
                Fecha = dto.Fecha,
                PrecioTotal = dto.PrecioTotal,
                usuario = usuarioOrden
            };

            var id = _OrdenRepo.AgregarOrden(ordenHecha);
            return CreatedAtAction(nameof(ConseguirOrden), new { id = id }, dto);
        }

        [HttpGet("{id}")]
        public IActionResult ConseguirOrden(int id)
        {
            var ordenEspecifica = _OrdenRepo.ObtenerOrdenPorID(id);
            return ordenEspecifica != null ? Ok(ordenEspecifica) : NotFound();
        }

        [HttpGet]
        public IActionResult ConseguirOrdenes()
        {
            var ordenes = _OrdenRepo.ObtenerOrdenes();
            return Ok(ordenes);
        }

        [HttpPost]
        public IActionResult PagarOrden(int id)
        {
            
        }
    }
}