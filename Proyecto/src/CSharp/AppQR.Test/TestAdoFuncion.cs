using System;
using Xunit;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios;
using AppQR.Dapper;

namespace AppQR.Test
{
    public class TestAdoFuncion : TestAdo
    {
        private IFuncionRepositorio _funcionRepositorio;

        public TestAdoFuncion()
        {
            _funcionRepositorio = new FuncionRepositorio(Conexion);
        }
        [Fact]
        public void CuandoAgrego_Una_Funcion_SeGuarda_En_LaBD()
        {
            var funcion = new Funcion()
            {
                IdFuncion = 10,
                FechaHora = DateTime.Parse("2023-12-31 20:00:00"),
                evento = new Evento { IdEvento = 1 },
                Estado = "Activo"

            };
            _funcionRepositorio.AgregarFuncion(funcion);

            var funcionAgregada = _funcionRepositorio.AgregarFuncion(funcion);

            Assert.NotNull(funcionAgregada);
            Assert.Equal(funcion.IdFuncion, funcionAgregada.IdFuncion);
            Assert.Equal(funcion.FechaHora, funcionAgregada.FechaHora);
            Assert.Equal(funcion.evento.IdEvento, funcionAgregada.evento.IdEvento);
            Assert.Equal(funcion.Estado, funcionAgregada.Estado);
            Assert.True(funcionAgregada.IdFuncion > 0);
        }
        [Fact]
        public void CuandoAgrego_Una_Funcion_Obtengo_UnID()
        {
            var funcion = new Funcion()
            {
                IdFuncion = 10,
                FechaHora = DateTime.Parse("2023-12-31 20:00:00"),
                evento = new Evento { IdEvento = 1 },
                Estado = "Activo"
            };
            _funcionRepositorio.AgregarFuncion(funcion);

            var funcionObtenida = _funcionRepositorio.ObtenerPorID(10);
            Assert.NotNull(funcionObtenida);
            Assert.Equal(funcion.IdFuncion, funcionObtenida.IdFuncion);
            Assert.Equal(funcion.FechaHora, funcionObtenida.FechaHora);
            Assert.Equal(funcion.evento.IdEvento, funcionObtenida.evento.IdEvento);
            Assert.Equal(funcion.Estado, funcionObtenida.Estado);
        }
        [Fact]
        public void CuandoActualizo_Una_Funcion_Hay_Cambios()
        {
            var funcion = new Funcion()
            {
                IdFuncion = 10,
                FechaHora = DateTime.Parse("2023-12-31 20:00:00"),
                evento = new Evento { IdEvento = 1 },
                Estado = "Activo"
            };
            _funcionRepositorio.AgregarFuncion(funcion);

            var funcionUpdate = new Funcion()
            {
                IdFuncion = 10,
                FechaHora = DateTime.Parse("2024-01-01 21:00:00"),
                evento = new Evento { IdEvento = 1 },
                Estado = "Inactivo"
            };

            _funcionRepositorio.ActualizarFuncion(funcionUpdate);
            var funcionActualizadaBD = _funcionRepositorio.ObtenerPorID(funcionUpdate.IdFuncion);

            Assert.NotNull(funcionActualizadaBD);
            Assert.Equal(funcion.IdFuncion, funcionUpdate.IdFuncion);
            Assert.Equal(funcionActualizadaBD.FechaHora, funcionUpdate.FechaHora);
            Assert.Equal(funcionActualizadaBD.evento.IdEvento, funcionUpdate.evento.IdEvento);
            Assert.Equal(funcionActualizadaBD.Estado, funcionUpdate.Estado);
        }

        [Fact]
        public void CuandoElimino_Una_Funcion_No_Debe_Existir_EnLaBD()
        {
            var funcion = new Funcion()
            {
                IdFuncion = 10,
                FechaHora = DateTime.Parse("2023-12-31 20:00:00"),
                evento = new Evento { IdEvento = 1 },
                Estado = "Activo"
            };
            _funcionRepositorio.AgregarFuncion(funcion);

            _funcionRepositorio.EliminarFuncion(funcion.IdFuncion);
            var funcionEliminadaBD = _funcionRepositorio.ObtenerPorID(funcion.IdFuncion);

            Assert.Null(funcionEliminadaBD);
        }

        [Fact]
        public void CuandoCancelo_Una_Funcion_Su_Estado_Cambia_A_Cancelado()
        {
            var funcion = new Funcion()
            {
                IdFuncion = 10,
                FechaHora = DateTime.Parse("2023-12-31 20:00:00"),
                evento = new Evento { IdEvento = 1 },
                Estado = "Activo"
            };
            _funcionRepositorio.AgregarFuncion(funcion);

            _funcionRepositorio.CancelarFuncion(funcion.IdFuncion);
            var funcionCanceladaBD = _funcionRepositorio.ObtenerPorID(funcion.IdFuncion);

            Assert.Equal("Cancelado", funcionCanceladaBD.Estado);
        }
    }
}


