using Microsoft.AspNetCore.Mvc;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios;
using System.Collections.Generic;

namespace AppQR.WebAPI.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesControlador : ControllerBase
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClientesControlador(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }


        // Post de Clientes /api/clientes
        [HttpPost]
        public IActionResult CrearCliente([FromBody] Cliente cliente)
        {
            try
            {
                if (cliente == null)
                {
                    return BadRequest("Los datos si o si deben ser ingresados.");
                }

                if (cliente.DNI <= 0)
                {
                    return BadRequest("Número de DNI inválido.");
                }

                var clienteExistente = _clienteRepositorio.ObtenerClientePorDNI(cliente.DNI);
                if (clienteExistente != null)
                {
                    return Conflict("Ya existe un cliente con ese DNI.");
                }

                var clienteCreado = _clienteRepositorio.AgregarCliente(cliente);
                return CreatedAtAction(nameof(ObtenerClientePorDNI), new { dni = clienteCreado.DNI }, clienteCreado);
            }
            catch
            {
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        // Get de todos los Clientes /api/clientes

        [HttpGet]
        public IActionResult ObtenerClientes()
        {
            try
            {
                var clientes = _clienteRepositorio.ObtenerClientes();
                return Ok(clientes);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
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
            try
            {
                if (dni != cliente.DNI)
                {
                    return BadRequest("El DNI no coincide con el de ningún cliente.");
                }

                if (cliente == null)
                {
                    return BadRequest("Los datos si o si deben ser ingresados.");
                }

                if (dni <= 0)
                {
                    return BadRequest("Número de DNI inválido.");
                }

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
            catch
            {
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        //DELETE de Cliente /api/clientes/{dni}
        [HttpDelete("{dni}")]
        public IActionResult EliminarCliente(int dni)
        {
            try
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
            catch
            {
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}