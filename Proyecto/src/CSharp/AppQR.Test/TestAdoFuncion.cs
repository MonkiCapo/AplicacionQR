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
    public class TestFuncionService
    {
        [Fact]
        public void Debe_DevolverTodas_LasFunciones_En_UnaLista()
        {
            var MOQ = new Mock<IFuncionService>();
            var funciones = new List<Funcion>
            {
                new Funcion { IdFuncion = 1, Nombre = "Concierto rock", FechaHora = DateTime.Parse("2023-12-31 20:00:00"), evento = new Evento { IdEvento = 1 }, Estado =  EEstados.Publicado},
                new Funcion { IdFuncion = 2, Nombre = "Concierto de pop", FechaHora = DateTime.Parse("2024-01-15 19:30:00"), evento = new Evento { IdEvento = 2 }, Estado = EEstados.Pendiente}
            };
            MOQ.Setup(s => s.ObtenerTodasLasFunciones()).Returns(funciones);

            var resultado = MOQ.Object.ObtenerTodasLasFunciones();

            Assert.NotNull(resultado);
            Assert.Equal(2, ((List<Funcion>)resultado).Count);
        }

        [Fact]
        public void Debe_Devolver_Funcion_Por_ID()
        {
            var MOQ = new Mock<IFuncionService>();
            var funcion = new Funcion
            {
                IdFuncion = 2,
                Nombre = "Concierto de gatos",
                FechaHora = DateTime.Parse("2024-01-15 19:30:00"),
                evento = new Evento { IdEvento = 2 },
                Estado = EEstados.Cancelado
            };
            MOQ.Setup(s => s.ObtenerPorID(2)).Returns(funcion);

            var resultado = MOQ.Object.ObtenerPorID(2);

            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.IdFuncion);
            Assert.Equal("Concierto de gatos", resultado.Nombre);
            Assert.Equal(DateTime.Parse("2024-01-15 19:30:00"), resultado.FechaHora);
            Assert.Equal(2, resultado.evento.IdEvento);
            Assert.Equal(EEstados.Cancelado, resultado.Estado);
        }

        [Fact]
        public void Debe_Agregar_Una_NuevaFuncion()
        {
            var MOQ = new Mock<IFuncionService>();
            var nuevaFuncion = new FuncionDTO
            {
                idEvento = 3,
                Nombre = "Obra musical",
                FechaHora = DateTime.Parse("2024-02-20 18:00:00"),
                Estado = EEstados.Publicado.ToString()
            };
            MOQ.Setup(s => s.AgregarFuncion(nuevaFuncion)).Returns(new Funcion { evento = new Evento { IdEvento = nuevaFuncion.idEvento }, Nombre = nuevaFuncion.Nombre, FechaHora = nuevaFuncion.FechaHora, Estado = EEstados.Publicado });

            var resultado = MOQ.Object.AgregarFuncion(nuevaFuncion);

            Assert.NotNull(resultado);
            Assert.Equal(3, resultado.evento.IdEvento);
            Assert.Equal("Obra musical", resultado.Nombre);
            Assert.Equal(DateTime.Parse("2024-02-20 18:00:00"), resultado.FechaHora);
            Assert.Equal(EEstados.Publicado, resultado.Estado);
        }

        [Fact]
        public void Debe_Actualizar_UnaFuncion_QueExiste()
        {
            var MOQ = new Mock<IFuncionService>();
            var funcion = new FuncionDTO
            {
                idEvento = 2,
                Nombre = "Concierto de rock",
                FechaHora = DateTime.Parse("2024-03-10 20:00:00"),
                Estado = EEstados.Activo.ToString()
            };
            var funcionUpdate = new FuncionDTO
            {
                idEvento = 2,
                Nombre = "Concierto de metal",
                FechaHora = DateTime.Parse("2024-05-12 21:00:00"),
                Estado = EEstados.Activo.ToString()
            };
            MOQ.Setup(s => s.ActualizarFuncion(funcionUpdate, 1)).Returns(true);

            var resultado = MOQ.Object.ActualizarFuncion(funcionUpdate, 1);

            Assert.True(resultado);
            Assert.Equal(2, funcionUpdate.idEvento);
            Assert.Equal("Concierto de metal", funcionUpdate.Nombre);
            Assert.Equal(DateTime.Parse("2024-05-12 21:00:00"), funcionUpdate.FechaHora);
            Assert.Equal(EEstados.Activo.ToString(), funcionUpdate.Estado);
        }

        [Fact]
        public void Debe_Eliminar_Una_Funcion_QueExiste()
        {
            var MOQ = new Mock<IFuncionService>();
            var funcion = new Funcion { IdFuncion = 1 };
            MOQ.Setup(s => s.EliminarFuncion(1)).Returns(true);

            var resultado = MOQ.Object.EliminarFuncion(1);

            Assert.True(resultado);
        }

        [Fact]
        public void Debe_Cancelar_Una_Funcion_QueYaExiste()
        {
            var MOQ = new Mock<IFuncionService>();
            var funcion = new Funcion { IdFuncion = 2 };
            MOQ.Setup(s => s.CancelarFuncion(2)).Returns("La funcion a sido cancelada");

            var resultado = MOQ.Object.CancelarFuncion(2);

            Assert.Equal("La funcion a sido cancelada", resultado);
        }

        
    }
}