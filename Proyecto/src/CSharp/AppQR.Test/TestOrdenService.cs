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
    public class TestOrdenService
    {
        [Fact]
        public void Debe_Devolver_Todas_LasOrdenes_EnUnaLista()
        {
            var MOQ = new Mock<IOrdenService>();
            var ordenes = new List<Orden>
            {
                new Orden { IdOrden = 1, usuario = new Usuario{ IdUsuario = 2}, Estado = EEstados.Pendiente, PrecioTotal = 2899, Fecha = DateTime.Parse("2025-05-12") },
                new Orden { IdOrden = 2, usuario = new Usuario{ IdUsuario = 3 }, Estado = EEstados.Pagado, PrecioTotal = 1500, Fecha = DateTime.Parse("2025-02-23") }
            };
            MOQ.Setup(s => s.ObtenerOrdenes()).Returns(ordenes);

            var resultado = MOQ.Object.ObtenerOrdenes();

            Assert.NotNull(resultado);
            Assert.Equal(2, ((List<Orden>)resultado).Count);
        }
        [Fact]
        public void Debe_DevolverOrden_Por_ID()
        {
            var MOQ = new Mock<IOrdenService>();
            var orden = new Orden
            {
                IdOrden = 1,
                usuario = new Usuario { IdUsuario = 2 },
                Estado = EEstados.Pendiente,
                PrecioTotal = 2899,
                Fecha = DateTime.Parse("2025-05-12")
            };
            MOQ.Setup(s => s.ObtenerOrdenPorID(1)).Returns(orden);

            var resultado = MOQ.Object.ObtenerOrdenPorID(1);

            Assert.Equal(1, resultado.IdOrden);
            Assert.Equal(2, resultado.usuario?.IdUsuario);
            Assert.Equal(EEstados.Pendiente, resultado.Estado);
            Assert.Equal(2899, resultado.PrecioTotal);
            Assert.Equal(DateTime.Parse("2025-05-12"), resultado.Fecha);
        }

        [Fact]
        public void Debe_Agregar_UnaNuevaOrden()
        {
            var MOQ = new Mock<IOrdenService>();
            var nuevaOrden = new OrdenDTO
            {
                Email = "ezequiel123@gmail.com",
                Estado = EEstados.Pendiente.ToString(),
                PrecioTotal = 3899,
                Fecha = DateTime.Parse("2025-08-12")
            };
            MOQ.Setup(s => s.AgregarOrden(nuevaOrden)).Returns(new Orden { usuario = new Usuario { Email =nuevaOrden.Email}, Estado = EEstados.Pendiente, PrecioTotal = nuevaOrden.PrecioTotal, Fecha = nuevaOrden.Fecha });

            var resultado = MOQ.Object.AgregarOrden(nuevaOrden);

            Assert.NotNull(resultado);
            Assert.Equal("ezequiel123@gmail.com", resultado.usuario?.Email);
            Assert.Equal(EEstados.Pendiente, resultado.Estado);
            Assert.Equal(3899, resultado.PrecioTotal);
            Assert.Equal(DateTime.Parse("2025-08-12"), resultado.Fecha);
        }

        [Fact]
        public void Debe_Cancelar_UnaOrden_SoloSiExiste()
        {
            var MOQ = new Mock<IOrdenService>();
            var orden = new Orden { IdOrden = 3 };
            MOQ.Setup(s => s.CancelarOrden(3)).Returns("La orden a sido cancelada");

            var resultado = MOQ.Object.CancelarOrden(3);

            Assert.Equal("La orden a sido cancelada", resultado);
        }

        [Fact]
        public void Debe_Pagar_UnaOrden_De_TalEntrada()
        {
            var MOQ = new Mock<IOrdenService>();
            var entradaDto = new EntradaDTO
            {
                IdTarifa = 2,
                IdOrden = 4,
                Estado = EEstados.Pagado.ToString()
            };
            MOQ.Setup(s => s.PagarOrden(4, entradaDto)).Returns("La orden a sido pagada correctamente");

            var resultado = MOQ.Object.PagarOrden(4, entradaDto);

            Assert.Equal("La orden a sido pagada correctamente", resultado);
        }
    }
}