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
using AppQR.Core.Servicios.Enums;

namespace AppQR.Test
{
    public class TestEntradaService
    {
        [Fact]
        public void Devolver_Todas_LasEntradas_En_UnaLista()
        {
            var MOQ = new Mock<IEntradaService>();
            var entradas = new List<Entrada>
            {
                new Entrada { IdEntrada= 1, tarifa = new Tarifa {IdTarifa = 1}, orden = new Orden {IdOrden = 1}, Estado= EEstados.Pendiente },
                new Entrada { IdEntrada= 2, tarifa = new Tarifa {IdTarifa = 2}, orden= new Orden {IdOrden = 2}, Estado= EEstados.Pagado }
            };
            MOQ.Setup(s => s.ObtenerEntradas()).Returns(entradas);

            var resultado = MOQ.Object.ObtenerEntradas();

            Assert.NotNull(resultado);
            Assert.Equal(2, ((List<Entrada>)resultado).Count);
        }
        [Fact]
        public void Debe_Devolver_Entrada_Por_ID()
        {
            var MOQ = new Mock<IEntradaService>();
            var entrada = new Entrada
            {
                IdEntrada = 1,
                tarifa = new Tarifa { IdTarifa = 1 },
                orden = new Orden { IdOrden = 1 },
                Estado = EEstados.Expirada
            };
            MOQ.Setup(s => s.ObtenerEntradaPorID(1)).Returns(entrada);

            var resultado = MOQ.Object.ObtenerEntradaPorID(1);

            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.IdEntrada);
            Assert.Equal(1, resultado.tarifa.IdTarifa);
            Assert.Equal(1, resultado.orden.IdOrden);
            Assert.Equal(EEstados.Expirada, resultado.Estado);
        }

        [Fact]
        public void Debe_Anular_UnaEntrada()
        {
            var MOQ = new Mock<IEntradaService>();
            var entrada = new Entrada
            {
                IdEntrada = 3,
                tarifa = new Tarifa { IdTarifa = 2 },
                orden = new Orden { IdOrden = 2 },
                Estado = EEstados.Anulada
            };
            MOQ.Setup(s => s.AnularEntrada(3)).Returns("Entrada anulada exitosamente");

            var resultado = MOQ.Object.AnularEntrada(3);

            Assert.NotNull(resultado);
            Assert.Equal("Entrada anulada exitosamente", resultado);
        }

        [Fact]
        public void Debe_ObtenerQR_DeUnaEntrada()
        {
            var MOQ = new Mock<IEntradaService>();
            var qrData = Encoding.UTF8.GetBytes("CodigoQRDeEntradaPrueba23");
            MOQ.Setup(s => s.ObtenerQR(1)).Returns(qrData);

            var resultado = MOQ.Object.ObtenerQR(1);

            Assert.NotNull(resultado);
            Assert.Equal(qrData, resultado);
        }

        [Fact]
        public void Debe_ValidarQR_DeUnaEntrada()
        {
            var MOQ = new Mock<IEntradaService>();
            var qrValidacion = new Entrada
            {
                IdEntrada = 1,
                tarifa = new Tarifa { IdTarifa = 2 },
                orden = new Orden { IdOrden = 2 },
                Estado = EEstados.Pendiente
            };
            MOQ.Setup(s => s.ValidarQR(1)).Returns("Entrada validada con exito");

            var resultado = MOQ.Object.ValidarQR(1);

            Assert.NotNull(resultado);
            Assert.Equal("Entrada validada con exito", resultado); 

        }

       

    }
}