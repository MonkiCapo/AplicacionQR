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
    public class TestEventoService
    {
        [Fact]
        public void Debe_Devolver_Todos_LosEventos_EnLista()
        {
            var MOQ = new Mock<IEventoService>();
            var eventos = new List<Evento>
            {
                new Evento { IdEvento = 1, Nombre = "Musical", Estado = EEstados.Cancelado },
                new Evento { IdEvento = 2, Nombre = "Carrera de bicicletas", Estado = EEstados.Publicado }
            };
            MOQ.Setup(s => s.ObtenerEventos()).Returns(eventos);

            var resultado = MOQ.Object.ObtenerEventos();

            Assert.NotNull(resultado);
            Assert.Equal(2, ((List<Evento>)resultado).Count);
        }

        [Fact]
        public void Debe_Devolver_Evento_Por_ID()
        {
            var MOQ = new Mock<IEventoService>();
            var evento = new Evento
            {
                IdEvento = 1,
                Nombre = "Evento de gatos bailarines",
                Estado = EEstados.Pendiente
            };
            MOQ.Setup(s => s.ObtenerEventoPorID(1)).Returns(evento);

            var resultado = MOQ.Object.ObtenerEventoPorID(1);

            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.IdEvento);
            Assert.Equal("Evento de gatos bailarines", resultado.Nombre);
            Assert.Equal(EEstados.Pendiente, resultado.Estado);
        }

        [Fact]
        public void Debe_Agregar_Un_NuevoEvento()
        {
            var MOQ = new Mock<IEventoService>();
            var nuevoEvento = new EventoDTO
            {
                Nombre = "Gatos bailarines",
                FechaInicio = DateTime.Parse("2025-05-20"),
                FechaFin = DateTime.Parse("2025-05-22")
            };
            MOQ.Setup(s => s.AgregarEvento(nuevoEvento)).Returns(new Evento { Nombre = nuevoEvento.Nombre, FechaInicio = nuevoEvento.FechaInicio, FechaFin = nuevoEvento.FechaFin });

            var resultado = MOQ.Object.AgregarEvento(nuevoEvento);

            Assert.NotNull(resultado);
            Assert.Equal("Gatos bailarines", resultado.Nombre);
            Assert.Equal(DateTime.Parse("2025-05.20"), resultado.FechaInicio);
            Assert.Equal(DateTime.Parse("2025-05-22"), resultado.FechaFin);
        }

        [Fact]
        public void Debe_Actualizar_Un_Evento_QueExiste()
        {
            var MOQ = new Mock<IEventoService>();
            var evento = new Evento
            {
                IdEvento = 3,
                Nombre = "Desfile de carrozas",
                FechaInicio = DateTime.Parse("2020-12-23"),
                FechaFin = DateTime.Parse("2020-12-24")
            };
            var eventoUpdate = new EventoDTO
            {
                Nombre = "Desfile de moda",
                FechaInicio = DateTime.Parse("2021-10-12"),
                FechaFin = DateTime.Parse("2021-10-14")
            };
            MOQ.Setup(s => s.ActualizarEvento(eventoUpdate, 3)).Returns(true);

            var resultado = MOQ.Object.ActualizarEvento(eventoUpdate, 3);

            Assert.True(resultado);
            Assert.Equal("Desfile de moda", eventoUpdate.Nombre);
            Assert.Equal(DateTime.Parse("2021-10-12"), eventoUpdate.FechaInicio);
            Assert.Equal(DateTime.Parse("2021-10-14"), eventoUpdate.FechaFin);
        }

        [Fact]
        public void Debe_Eliminar_Un_Evento_QueYaExiste()
        {
            var MOQ = new Mock<IEventoService>();
            var evento = new Evento { IdEvento = 4 };
            MOQ.Setup(s => s.EliminarEvento(4)).Returns(true);

            var resultado = MOQ.Object.EliminarEvento(4);

            Assert.True(resultado);
        }

        [Fact]
        public void Debe_Cancelar_Un_EventoExistente()
        {
            var MOQ = new Mock<IEventoService>();
            var evento = new Evento { IdEvento = 2 };
            MOQ.Setup(s => s.CancelarEvento(2)).Returns("El evento a sido cancelado");

            var resultado = MOQ.Object.CancelarEvento(2);

            Assert.Equal("El evento a sido cancelado", resultado);

        }

        [Fact]
        public void Debe_Publicar_UnEventoExistente()
        {
            var MOQ = new Mock<IEventoService>();
            var evento = new Evento { IdEvento = 3 };
            MOQ.Setup(s => s.PublicarEvento(3)).Returns("El evento a sido publicado");

            var resultado = MOQ.Object.PublicarEvento(3);

            Assert.Equal("El evento a sido publicado", resultado);
        }
    }
}