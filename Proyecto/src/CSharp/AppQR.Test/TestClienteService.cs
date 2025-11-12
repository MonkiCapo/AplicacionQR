using System;
using Xunit;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios;
using AppQR.Services;
using AppQR.Core.Dto;
using Moq;
using AppQR.Core.Servicios.IServicios;
using System.Linq.Expressions;
using MySqlX.XDevAPI.Common;
using System.Text;

namespace AppQR.Test
{
    public class TestClienteService
    {
        [Fact]
        public void ObtenerTodos_Los_Clientes_En_Una_Lista()
        {
            var MOQ = new Mock<IClienteService>();
            var clientes = new List<Cliente>
            {
                new Cliente { DNI = 12345678, Nombre = "Mia Gomez", Telefono = "1134567893"},
                new Cliente { DNI = 87654321, Nombre = "Carlos Gomez", Telefono = "1176543212"}
            };
            MOQ.Setup(s => s.ObtenerClientes()).Returns(clientes);

            var resultado = MOQ.Object.ObtenerClientes();

            Assert.NotNull(resultado);
            Assert.Equal(2, ((List<Cliente>)resultado).Count);
        }

        [Fact]
        public void Debe_Devolver_Cliente_Por_DNI()
        {
            var MOQ = new Mock<IClienteService>();
            var cliente = new Cliente
            {
                DNI = 12345678,
                Nombre = "Mia Gomez",
                Telefono = "1134567893"
            };
            MOQ.Setup(s => s.ObtenerClientePorDNI(12345678)).Returns(cliente);

            var resultado = MOQ.Object.ObtenerClientePorDNI(12345678);

            Assert.NotNull(resultado);
            Assert.Equal(12345678, resultado.DNI);
            Assert.Equal("Mia Gomez", resultado.Nombre);
            Assert.Equal("1134567893", resultado.Telefono);
        }

        [Fact]
        public void Debe_Agregar_Un_NuevoCliente()
        {
            var MOQ = new Mock<IClienteService>();
            var nuevoCliente = new ClienteDTO
            {
                DNI = 47654321,
                Nombre = "Selena Gomez",
                Telefono = "1137483163"
            };
            MOQ.Setup(s => s.AgregarCliente(nuevoCliente)).Returns(new Cliente { DNI = nuevoCliente.DNI, Nombre = nuevoCliente.Nombre, Telefono = nuevoCliente.Telefono });

            var resultado = MOQ.Object.AgregarCliente(nuevoCliente);

            Assert.NotNull(resultado);
            Assert.Equal(47654321, resultado.DNI);
            Assert.Equal("Selena Gomez", resultado.Nombre);
            Assert.Equal("1137483163", resultado.Telefono);
        }

        [Fact]
        public void Debe_Actualizar_Un_Cliente_QueExiste()
        {
            var MOQ = new Mock<IClienteService>();
            var cliente = new ClienteDTO
            {
                DNI = 47654321,
                Nombre = "Morena Gomez",
                Telefono = "1137483163"
            };
            var clienteUpdate = new ClienteActualizadoDTO
            {
                Nombre = "More Gomez",
                Telefono = "1147157455"
            };
            MOQ.Setup(s => s.ActualizarCliente(clienteUpdate, 47654321)).Returns(true);

            var resultado = MOQ.Object.ActualizarCliente(clienteUpdate, 47654321);

            Assert.True(resultado);
            Assert.Equal("More Gomez", clienteUpdate.Nombre);
            Assert.Equal("1147157455", clienteUpdate.Telefono);
        }

        [Fact]
        public void Debe_Eliminar_Un_Cliente_QueExiste()
        {
            var MOQ = new Mock<IClienteService>();
            var cliente = new Cliente { DNI = 47654321 };
            MOQ.Setup(s => s.EliminarCliente(47654321)).Returns(true);

            var resultado = MOQ.Object.EliminarCliente(47654321);

            Assert.True(resultado);
        }

        [Fact]
        public void Debe_Verificar_Si_Existe_DNI_De_Algun_Cliente()
        {
            var MOQ = new Mock<IClienteService>();
            var cliente = new Cliente { DNI = 12345678 };
            MOQ.Setup(s => s.ExisteDNIdeCliente(12345678)).Returns(true);

            var resultado = MOQ.Object.ExisteDNIdeCliente(12345678);

            Assert.True(resultado);
        }
    
    }
}