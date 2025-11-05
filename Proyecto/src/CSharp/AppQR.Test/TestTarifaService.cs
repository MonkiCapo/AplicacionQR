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
    public class TestTarifaService
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
    }
}