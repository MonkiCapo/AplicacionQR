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
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace AppQR.Test
{
    public class TestAdoLocal
    {
        [Fact]
        public void Debe_DevolverTodos_Los_Locales_En_UnaLista()
        {
            var MOQ = new Mock<ILocalService>();
            var locales = new List<Local>
            {
                new Local { IdLocal = 1, Nombre = "Luna Park", Direccion = "Av. Corrientes 232" },
                new Local { IdLocal = 2, Nombre = "Estadio Musical", Direccion = "Av. DeCats 238" }
            };
            MOQ.Setup(s => s.ObtenerLocales()).Returns(locales);

            var resultado = MOQ.Object.ObtenerLocales();

            Assert.NotNull(resultado);
            Assert.Equal(2, ((List<Local>)resultado).Count);
        }

        [Fact]
        public void Debe_Devolver_Local_Por_ID()
        {
            var MOQ = new Mock<ILocalService>();
            var local = new Local
            {
                IdLocal = 1,
                Nombre = "Luna Park",
                Direccion = "Av. Corrientes 232"
            };
            MOQ.Setup(s => s.ObtenerLocalPorID(1)).Returns(local);

            var resultado = MOQ.Object.ObtenerLocalPorID(1);

            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.IdLocal);
            Assert.Equal("Luna Park", resultado.Nombre);
            Assert.Equal("Av. Corrientes 232", resultado.Direccion);
        }

        [Fact]
        public void Cuando_Agrego_UnNuevoLocal_Se_DebeGuardar()
        {
            var MOQ = new Mock<ILocalService>();
            var nuevoLocal = new LocalDTO
            {
                Nombre = "Lollapalooza",
                Direccion = "Av. Mayo 66"
            };
            MOQ.Setup(s => s.AgregarLocal(nuevoLocal)).Returns(new Local { Nombre = nuevoLocal.Nombre, Direccion = nuevoLocal.Direccion });

            var resultado = MOQ.Object.AgregarLocal(nuevoLocal);

            Assert.NotNull(resultado);
            Assert.Equal("Lollapalooza", resultado.Nombre);
            Assert.Equal("Av. Mayo 66", resultado.Direccion);
        }

        [Fact]
        public void Debe_Actualizar_UnLocal_QueYaExiste()
        {
            var MOQ = new Mock<ILocalService>();
            var local = new Local
            {
                IdLocal = 4,
                Nombre = "Lollapalooza",
                Direccion = "Av.Cats 78"
            };
            var localUpdate = new LocalDTO
            {
                Nombre = "Teatro musical",
                Direccion = "Av. Rivadavia 024"
            };
            MOQ.Setup(s => s.ActualizarLocal(localUpdate, 4)).Returns(true);

            var resultado = MOQ.Object.ActualizarLocal(localUpdate, 4);

            Assert.True(resultado);
            Assert.Equal("Teatro musical", localUpdate.Nombre);
            Assert.Equal("Av. Rivadavia 024", localUpdate.Direccion);
        }

        [Fact]
        public void Debe_Eliminar_UnLocal_QueExiste()
        {
            var MOQ = new Mock<ILocalService>();
            var local = new Local { IdLocal = 5 };
            MOQ.Setup(s => s.EliminarLocal(5)).Returns(true);

            var resultado = MOQ.Object.EliminarLocal(5);

            Assert.True(resultado);
        }

        [Fact]
        public void Debe_DevolverTodos_Los_Sectores_DeLocal_En_UnaLista()
        {
            var MOQ = new Mock<ILocalService>();
            var sectores = new List<Sector>
            {
                new Sector { IdSector = 1, Capacidad = 500, local = new Local { IdLocal = 1}},
                new Sector { IdSector = 2, Capacidad = 300, local = new Local { IdLocal = 1}}
            };
            MOQ.Setup(s => s.ObtenerSectoresPorLocal(1)).Returns(sectores);

            var resultado = MOQ.Object.ObtenerSectoresPorLocal(1);

            Assert.NotNull(resultado);
            Assert.Equal(2, ((List<Sector>)resultado).Count);
        }

        [Fact]
        public void Debe_Devolver_Sector_Por_ID()
        {
            var MOQ = new Mock<ILocalService>();
            var sector = new Sector
            {
                IdSector = 1,
                Capacidad = 600,
                local = new Local { IdLocal = 2 }
            };
            MOQ.Setup(s => s.ObtenerSectorPorID(1)).Returns(sector);

            var resultado = MOQ.Object.ObtenerSectorPorID(1);

            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.IdSector);
            Assert.Equal(600, resultado.Capacidad);
            Assert.Equal(2, resultado.local.IdLocal);
        }

        [Fact]
        public void CuandoAgrego_UnNuevo_Sector_Se_DebeGuardar()
        {

            var MOQ = new Mock<ILocalService>();
            var sectorID = new Local { IdLocal = 1 };
            var nuevoSector = new SectorDTO
            {
                Capacidad = 203
            };
            MOQ.Setup(s => s.AgregarSector(nuevoSector, 1)).Returns(new Sector { Capacidad = nuevoSector.Capacidad });

            var resultado = MOQ.Object.AgregarSector(nuevoSector, 1);

            Assert.NotNull(resultado);
            Assert.Equal(203, resultado.Capacidad);
        }

        [Fact]
        public void Debe_Actualizar_UnSector_QueYaExiste()
        {
            var MOQ = new Mock<ILocalService>();
            var sector = new Sector
            {
                IdSector = 2,
                Capacidad = 345,
                local = new Local { IdLocal = 1 }
            };
            var sectorUpdate = new SectorDTO
            {
                Capacidad = 200
            };
            MOQ.Setup(s => s.ActualizarSector(sectorUpdate, 2)).Returns(true);

            var resultado = MOQ.Object.ActualizarSector(sectorUpdate, 2);

            Assert.True(resultado);
            Assert.Equal(200, sectorUpdate.Capacidad);
        }

        [Fact]
        public void Debe_Eliminar_UnSector_QueExiste()
        {
            var MOQ = new Mock<ILocalService>();
            var sector = new Sector { IdSector = 5 };
            MOQ.Setup(s => s.EliminarSector(5)).Returns(true);

            var resultado = MOQ.Object.EliminarSector(5);

            Assert.True(resultado);
        }
    }
}