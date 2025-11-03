using System;
using Xunit;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios;
using AppQR.Services;
using AppQR.Core.Dto;
using Moq;
using AppQR.Core.Servicios.IServicios;

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
            Assert.Equal(2,((List<Cliente>)resultado).Count);
            
        }
    }
}