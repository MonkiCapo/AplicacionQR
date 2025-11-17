using System;
using Xunit;
using AppQR.Core.Entidades;
using AppQR.Core.Dto;
using Moq;
using AppQR.Core.Servicios.IServicios;
using System.Text;
using AppQR.Core.Servicios.Enums;
using System.Collections.Generic;

namespace AppQR.Test
{
    public class TestEntradaService
    {
        [Fact]
        public void Devolver_Todas_LasEntradas_En_UnaLista()
        {
            var mock = new Mock<IEntradaService>();

            var entradas = new List<Entrada>
            {
                new Entrada { IdEntrada = 1, tarifa = new Tarifa { IdTarifa = 1 }, orden = new Orden { IdOrden = 1 }, Estado = EEstados.Pendiente },
                new Entrada { IdEntrada = 2, tarifa = new Tarifa { IdTarifa = 2 }, orden = new Orden { IdOrden = 2 }, Estado = EEstados.Pagado }
            };

            mock.Setup(s => s.ObtenerEntradas()).Returns(entradas);

            var resultado = mock.Object.ObtenerEntradas();

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public void Debe_Devolver_Entrada_Por_ID()
        {
            var mock = new Mock<IEntradaService>();

            var entrada = new Entrada
            {
                IdEntrada = 1,
                tarifa = new Tarifa { IdTarifa = 1 },
                orden = new Orden { IdOrden = 1 },
                Estado = EEstados.Expirada
            };

            mock.Setup(s => s.ObtenerEntradaPorID(1)).Returns(entrada);

            var resultado = mock.Object.ObtenerEntradaPorID(1);

            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.IdEntrada);
            Assert.Equal(1, resultado.tarifa.IdTarifa);
            Assert.Equal(1, resultado.orden.IdOrden);
            Assert.Equal(EEstados.Expirada, resultado.Estado);
        }

        [Fact]
        public void Debe_Anular_UnaEntrada()
        {
            var mock = new Mock<IEntradaService>();

            mock.Setup(s => s.AnularEntrada(3)).Returns("Entrada anulada exitosamente");

            var resultado = mock.Object.AnularEntrada(3);

            Assert.NotNull(resultado);
            Assert.Equal("Entrada anulada exitosamente", resultado);
        }

        [Fact]
        public void Debe_ObtenerQR_DeUnaEntrada()
        {
            var mock = new Mock<IEntradaService>();

            var qrData = Encoding.UTF8.GetBytes("QRPrueba123");
            mock.Setup(s => s.ObtenerQR(1)).Returns(qrData);

            var resultado = mock.Object.ObtenerQR(1);

            Assert.NotNull(resultado);
            Assert.Equal(qrData, resultado);
        }

        [Fact]
        public void Debe_ValidarQR_DeUnaEntrada()
        {
            // Arrange
            var mock = new Mock<IEntradaService>();
            string token = "TOKEN123";

            var respuesta = new
            {
                mensaje = "Entrada validada correctamente",
                idEntrada = 1
            };

            mock.Setup(s => s.ValidarQR(token)).Returns(respuesta);

            // Act
            var resultado = mock.Object.ValidarQR(token);

            // Assert
            Assert.NotNull(resultado);

            // Me veo obligado a usar dynamic para acceder a las propiedades sin problema, ya que el metodo devuelve un object
            dynamic obj = resultado;

            Assert.Equal("Entrada validada correctamente", (string)obj.mensaje);
            Assert.Equal(1, (int)obj.idEntrada);
        }
    }
}
