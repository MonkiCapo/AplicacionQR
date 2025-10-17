using Microsoft.AspNetCore.Mvc;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Servicios.IServicios;

namespace AppQR.WebAPI.Controladores
{
    [ApiController]
    [Authorize(Roles = "Usuario")]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService clienteService;

        public ClientesController(IClienteService clienteService)
        {
            this.clienteService = clienteService;
        }


        // Post de Clientes /api/clientes
        [HttpPost]
        public IActionResult CrearCliente([FromBody] Cliente cliente)
        {
            var clienteExistente = clienteService.ObtenerClientePorDNI(cliente.DNI);
            if (clienteExistente != null)
            {
                return Conflict("Ya existe un cliente con ese DNI.");
            }

            var clienteCreado = clienteService.AgregarCliente(cliente);
            return CreatedAtAction(nameof(ObtenerClientePorDNI), new { dni = clienteCreado.DNI }, clienteCreado);
        }

        // Get de todos los Clientes /api/clientes

        [HttpGet]

        public IActionResult ObtenerClientes()
        {
            var clientes = clienteService.ObtenerClientes();
            return Ok(clientes);
        }

        // // Get de Cliente por DNI /api/clientes/{dni}

        [HttpGet("{dni}")]
        public IActionResult ObtenerClientePorDNI(int dni)
        {
            var cliente = clienteService.ObtenerClientePorDNI(dni);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        // // PUT de Cliente /api/clientes/{dni}
        // [HttpPut("{dni}")]
        // public IActionResult ActualizarCliente(int dni, [FromBody] Cliente cliente)
        // {

        //         var clienteExistente = _clienteRepositorio.ObtenerClientePorDNI(dni);
        //         if (clienteExistente == null)
        //         {
        //             return NotFound("No se encontró un cliente con ese DNI.");
        //         }

        //         var actualizado = _clienteRepositorio.ActualizarCliente(cliente);
        //         if (actualizado)
        //             return Ok(cliente);
        //         else
        //             return StatusCode(500, "Error al actualizar el cliente.");
        // }

        // //DELETE de Cliente /api/clientes/{dni}
        // [HttpDelete("{dni}")]
        // public IActionResult EliminarCliente(int dni)
        // {

        //         var clienteExistente = _clienteRepositorio.ObtenerClientePorDNI(dni);
        //         if (clienteExistente == null)
        //         {
        //             return NotFound("No se encontró un cliente con ese DNI.");
        //         }

        //         var eliminado = _clienteRepositorio.EliminarCliente(dni);
        //         if (eliminado)
        //             return NoContent();
        //         else
        //             return StatusCode(500, "Error al eliminar el cliente.");
        // }
    }
}