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
    public class TestAdoTarifa
    {
        [Fact]
        public void Debe_Devolver_TodasLas_Tarifas_EnLista()
        {
            var MOQ = new Mock<ITarifaService>();
            var tarifas = new List<Tarifa>
            {
                new Tarifa { IdTarifa = 1, Tipo = ETipoTarifa.General, Precio = 1800, Stock = 50, Estado = EEstados.Expirada, funcion = new Funcion { IdFuncion = 1} },
                new Tarifa { IdTarifa = 2, Tipo = ETipoTarifa.Infantil, Precio = 1500, Stock = 30, Estado = EEstados.Anulada, funcion = new Funcion { IdFuncion =2} }
            };
            MOQ.Setup(s => s.ObtenerTodasLasTarifas()).Returns(tarifas);

            var resultado = MOQ.Object.ObtenerTodasLasTarifas();

            Assert.NotNull(resultado);
            Assert.Equal(2, ((List<Tarifa>)resultado).Count);
        }

        [Fact]
        public void Debe_Devolver_Tarifa_Por_ID()
        {
            var MOQ = new Mock<ITarifaService>();
            var tarifa = new Tarifa
            {
                IdTarifa = 1,
                Tipo = ETipoTarifa.VIP,
                Precio = 3000,
                Stock = 20,
                Estado = EEstados.Publicado,
                funcion = new Funcion { IdFuncion = 1 }
            };
            MOQ.Setup(s => s.ObtenerTarifaPorID(1)).Returns(tarifa);

            var resultado = MOQ.Object.ObtenerTarifaPorID(1);

            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.IdTarifa);
            Assert.Equal(ETipoTarifa.VIP, resultado.Tipo);
            Assert.Equal(3000, resultado.Precio);
            Assert.Equal(20, resultado.Stock);
            Assert.Equal(EEstados.Publicado, resultado.Estado);
            Assert.Equal(1, resultado.funcion.IdFuncion);
        }

        [Fact]
        public void Debe_ObtenerTarifas_PorFuncionID()
        {
            var MOQ = new Mock<ITarifaService>();
            var tarifas = new List<Tarifa>
            {
                new Tarifa  { IdTarifa = 2, Tipo = ETipoTarifa.General, Precio = 2998, Stock = 200, Estado = EEstados.Publicado, funcion = new Funcion { IdFuncion=5}},
                new Tarifa { IdTarifa = 3, Tipo = ETipoTarifa.VIP, Precio =3999, Stock = 28, Estado = EEstados.Cancelado, funcion = new Funcion { IdFuncion = 5}}
            };
            MOQ.Setup(s => s.ObtenerTarifasPorFuncion(5)).Returns(tarifas);

            var resultado = MOQ.Object.ObtenerTarifasPorFuncion(5);

            Assert.NotNull(resultado);
            Assert.Equal(2, ((List<Tarifa>)resultado).Count);
        }

        [Fact]
        public void Debe_Agregar_UnaNueva_Tarifa()
        {
            var MOQ = new Mock<ITarifaService>();
            var nuevaTarifa = new TarifaDTO
            {
                IdFuncion = 2,
                Tipo = ETipoTarifa.General.ToString(),
                Precio = 3999,
                Stock = 85
            };
            MOQ.Setup(s => s.AgregarTarifa(nuevaTarifa)).Returns(new Tarifa { funcion = new Funcion { IdFuncion = nuevaTarifa.IdFuncion }, Tipo = ETipoTarifa.General, Precio = nuevaTarifa.Precio, Stock = nuevaTarifa.Stock });

            var resultado = MOQ.Object.AgregarTarifa(nuevaTarifa);

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.funcion.IdFuncion);
            Assert.Equal(ETipoTarifa.General, resultado.Tipo);
            Assert.Equal(3999, resultado.Precio);
            Assert.Equal(85, resultado.Stock);
        }

        [Fact]
        public void Debe_Actualizar_UnaTarifa_Existente()
        {
            var MOQ = new Mock<ITarifaService>();
            var tarifa = new Tarifa
            {
                IdTarifa = 4,
                Tipo = ETipoTarifa.Infantil,
                Precio = 2599,
                Stock = 65,
                Estado = EEstados.Publicado,
                funcion = new Funcion { IdFuncion = 2 }
            };
            var tarifaUpdate = new TarifaDTO
            {
                IdFuncion = 2,
                Tipo = ETipoTarifa.VIP.ToString(),
                Precio = 6599,
                Stock = 45
            };
            MOQ.Setup(s => s.ActualizarTarifa(tarifaUpdate, 4)).Returns(true);

            var resultado = MOQ.Object.ActualizarTarifa(tarifaUpdate, 4);

            Assert.True(resultado);
            Assert.Equal(2, tarifaUpdate.IdFuncion);
            Assert.Equal(ETipoTarifa.VIP.ToString(), tarifaUpdate.Tipo);
            Assert.Equal(6599, tarifaUpdate.Precio);
            Assert.Equal(45, tarifaUpdate.Stock);
        }

        [Fact]
        public void Debe_Eliminar_UnaTarifa_QueExiste()
        {
            var MOQ = new Mock<ITarifaService>();
            var tarifa = new Tarifa { IdTarifa = 2 };
            MOQ.Setup(s => s.EliminarTarifa(2)).Returns(true);

            var resultado = MOQ.Object.EliminarTarifa(2);

            Assert.True(resultado);
        }
    }
}