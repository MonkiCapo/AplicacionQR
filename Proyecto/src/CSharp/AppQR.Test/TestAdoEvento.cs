using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios;
using AppQR.Dapper;
using MySql.Data.MySqlClient;
using Moq;
using System.Data;

namespace AppQR.Test
{
    public class TestAdoEvento
    {
        [Fact]
        public void Obtener_Todos_Los_Eventos_Con_Una_Lista()
        {
            var MOQ = new Mock<IEventosRepositorio>();
            var eventos = new List<Evento>
            {
                new Evento { IdEvento = 1, Nombre = "Los milaneseros" },
                new Evento { IdEvento = 2, Nombre = "Carrera de bicicletas" }
            };
            MOQ.Setup(r => r.ObtenerEventos()).Returns(eventos);

            var resultado = MOQ.Object.ObtenerEventos();

            Assert.NotNull(resultado);
            Assert.Equal(2, ((List<Evento>)resultado).Count);
        }

        [Fact]
        public void Debe_Devolver_Evento_Por_ID()
        {
            var MOQ = new Mock<IEventosRepositorio>();
            var evento = new Evento { IdEvento = 1, Nombre = "Concierto" };
            MOQ.Setup(r => r.ObtenerEventoPorID(1)).Returns(evento);

            var resultado = MOQ.Object.ObtenerEventoPorID(1);

            Assert.NotNull(resultado);
            Assert.Equal(1, resultado.IdEvento);
            Assert.Equal("Concierto", resultado.Nombre);
        }
    }
}