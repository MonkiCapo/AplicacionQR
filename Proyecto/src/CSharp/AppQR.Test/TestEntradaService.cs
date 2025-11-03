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
        public void Debe_Crear_Una_NuevaEntrada()
        {
            var MOQ = new Mock<IEntradaService>();
            var nuevaEntrada = new EntradaDTO
            {
                IdTarifa = 2,
                IdOrden = 3,
                Estado = EEstados.Pagado.ToString()
            };
            MOQ.Setup(s => s.AgregarEntrada(nuevaEntrada)).Returns(new Entrada { tarifa = new Tarifa { IdTarifa = nuevaEntrada.IdTarifa }, orden = new Orden { IdOrden = nuevaEntrada.IdOrden }, Estado = EEstados.Pagado });

            var resultado = MOQ.Object.AgregarEntrada(nuevaEntrada);

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.tarifa.IdTarifa);
            Assert.Equal(3, resultado.orden.IdOrden);
            Assert.Equal(EEstados.Pagado, resultado.Estado);
        }

        [Fact]
        public void Debe_Actualizar_UnaEntrada_QueExiste()
        {
            var MOQ = new Mock<IEntradaService>();
            var entrada = new EntradaDTO
            {
                IdTarifa = 3,
                IdOrden = 4,
                Estado = EEstados.Pagado.ToString()
            };
            var entradaUpdate = new EntradaDTO
            {
                IdTarifa = 3,
                IdOrden = 4,
                Estado = EEstados.Expirada.ToString()
            };
            MOQ.Setup(s => s.ActualizarEntrada(entradaUpdate, 1)).Returns(true);

            var resultado = MOQ.Object.ActualizarEntrada(entradaUpdate, 1);

            Assert.True(resultado);
            Assert.Equal(3, entradaUpdate.IdTarifa);
            Assert.Equal(4, entradaUpdate.IdOrden);
            Assert.Equal(EEstados.Expirada.ToString(), entradaUpdate.Estado);
        }

        [Fact]
        public void Debe_Eliminar_UnaEntrada_Existente()
        {
            var MOQ = new Mock<IEntradaService>();
            var entrada = new Entrada { IdEntrada = 1 };
            MOQ.Setup(s => s.EliminarEntrada(1)).Returns(true);

            var resultado = MOQ.Object.EliminarEntrada(1);

            Assert.True(resultado);
        }
    }
}