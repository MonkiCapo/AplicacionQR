using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios;
using AppQR.Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace AppQR.Test
{
    public class TestAdoEvento : TestAdo
    {

        private IEventosRepositorio _eventoRepositorio;

        public TestAdoEvento()
        {
            _eventoRepositorio = new EventosRepositorio(Conexion); 
        }

        [Fact]
        public void CuandoAgrego_Un_Evento_Se_Guarda_En_LaBD()
        {
            var evento = new Evento()
            {
                IdEvento = 150,
                Nombre = "Concierto Pop",
                Estado = "Activo",
                local = new Local { IdLocal = 1 }
            };
            _eventoRepositorio.AgregarEvento(evento);

            var eventoAgregado = _eventoRepositorio.AgregarEvento(evento);

            Assert.NotNull(eventoAgregado);
            Assert.Equal(evento.IdEvento, eventoAgregado.IdEvento);
            Assert.Equal(evento.Nombre, eventoAgregado.Nombre);
            Assert.Equal(evento.Estado, eventoAgregado.Estado);
            Assert.Equal(evento.local.IdLocal, eventoAgregado.local.IdLocal);
            Assert.True(eventoAgregado.IdEvento > 0);
        }
        [Fact]
        public void CuandoAgrego_Un_Evento_Obtengo_UnID()
        {
            var evento = new Evento()
            {
                IdEvento = 100,
                Nombre = "Concierto Rock",
                Estado = "Activo",
                local = new Local { IdLocal = 1 }
            };
            _eventoRepositorio.AgregarEvento(evento);

            var eventoObtenido = _eventoRepositorio.ObtenerEventoPorID(100);

            Assert.NotNull(eventoObtenido);
            Assert.Equal(evento.IdEvento, eventoObtenido.IdEvento);
            Assert.Equal(evento.Nombre, eventoObtenido.Nombre);
            Assert.Equal(evento.Estado, eventoObtenido.Estado);
            Assert.Equal(evento.local.IdLocal, eventoObtenido.local.IdLocal);
        }

        [Fact]
        public void CuandoActualizo_Un_Evento_Hay_Cambios()
        {
            var evento = new Evento()
            {
                IdEvento = 200,
                Nombre = "Concierto Kpop",
                Estado = "Activo",
                local = new Local { IdLocal = 1 }
            };
            _eventoRepositorio.AgregarEvento(evento);

            var eventoUpdate = new Evento()
            {
                IdEvento = 200,
                Nombre = "Concierto Actualizado",
                Estado = "inactivo",
                local = new Local { IdLocal = 1 }
            };

            _eventoRepositorio.ActualizarEvento(eventoUpdate);
            var eventoActualizaoBD = _eventoRepositorio.ObtenerEventoPorID(eventoUpdate.IdEvento);

            Assert.NotNull(eventoActualizaoBD);
            Assert.Equal(evento.IdEvento, eventoUpdate.IdEvento);
            Assert.Equal(eventoActualizaoBD.Nombre, eventoUpdate.Nombre);
            Assert.Equal(eventoActualizaoBD.Estado, eventoUpdate.Estado);
            Assert.Equal(eventoActualizaoBD.local.IdLocal, eventoUpdate.local.IdLocal);


        }

        [Fact]
        public void CuandoElimino_Un_Evento_No_Debe_Existir()
        {
            var evento = new Evento()
            {
                IdEvento = 208,
                Nombre = "Concierto de Gatos",
                Estado = "Activo",
                local = new Local { IdLocal = 1 }
            };
            _eventoRepositorio.AgregarEvento(evento);

            _eventoRepositorio.EliminarEvento(evento.IdEvento);
            var eventoEliminado = _eventoRepositorio.ObtenerEventoPorID(evento.IdEvento);

            Assert.Null(eventoEliminado);

        }
        [Fact]
        public void CuandoCancelo_Un_Evento_Su_Estado_Cambia_A_Cancelado()
        {
            var evento = new Evento()
            {
                IdEvento = 202,
                Nombre = "Concierto Pop",
                Estado = "Activo",
                local = new Local { IdLocal = 1 }
            };
            _eventoRepositorio.AgregarEvento(evento);

            _eventoRepositorio.CancelarEvento(evento.IdEvento);
            var eventoCancelado = _eventoRepositorio.ObtenerEventoPorID(evento.IdEvento);

            Assert.Equal("Cancelado", eventoCancelado.Estado);
        }
    }
}