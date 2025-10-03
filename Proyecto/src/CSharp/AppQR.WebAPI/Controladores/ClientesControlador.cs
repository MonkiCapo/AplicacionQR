using Microsoft.AspNetCore.Mvc;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios;
using System.Collections.Generic;

namespace AppQR.WebAPI.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClientesController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }


        // Post de Clientes /api/clientes
        [HttpPost]
        public IActionResult CrearCliente([FromBody] Cliente cliente)
        {
            

                var clienteExistente = _clienteRepositorio.ObtenerClientePorDNI(cliente.DNI);
                if (clienteExistente != null)
                {
                    return Conflict("Ya existe un cliente con ese DNI.");
                }

                var clienteCreado = _clienteRepositorio.AgregarCliente(cliente);
                return CreatedAtAction(nameof(ObtenerClientePorDNI), new { dni = clienteCreado.DNI }, clienteCreado);
        }

        // Get de todos los Clientes /api/clientes

        [HttpGet]
        public IActionResult ObtenerClientes()
        {
                var clientes = _clienteRepositorio.ObtenerClientes();
                return Ok(clientes);
        }

        // Get de Cliente por DNI /api/clientes/{dni}

        [HttpGet("{dni}")]
        public IActionResult ObtenerClientePorDNI(int dni)
        {
            var cliente = _clienteRepositorio.ObtenerClientePorDNI(dni);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        // PUT de Cliente /api/clientes/{dni}
        [HttpPut("{dni}")]
        public IActionResult ActualizarCliente(int dni, [FromBody] Cliente cliente)
        {

                var clienteExistente = _clienteRepositorio.ObtenerClientePorDNI(dni);
                if (clienteExistente == null)
                {
                    return NotFound("No se encontró un cliente con ese DNI.");
                }

                var actualizado = _clienteRepositorio.ActualizarCliente(cliente);
                if (actualizado)
                    return Ok(cliente);
                else
                    return StatusCode(500, "Error al actualizar el cliente.");
        }

        //DELETE de Cliente /api/clientes/{dni}
        [HttpDelete("{dni}")]
        public IActionResult EliminarCliente(int dni)
        {

                var clienteExistente = _clienteRepositorio.ObtenerClientePorDNI(dni);
                if (clienteExistente == null)
                {
                    return NotFound("No se encontró un cliente con ese DNI.");
                }

                var eliminado = _clienteRepositorio.EliminarCliente(dni);
                if (eliminado)
                    return NoContent();
                else
                    return StatusCode(500, "Error al eliminar el cliente.");
        }
    }
}