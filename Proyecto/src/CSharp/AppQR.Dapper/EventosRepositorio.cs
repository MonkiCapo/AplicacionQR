using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using AppQR.Core;
using AppQR.Core.Servicios;
using AppQR.Core.Entidades;
using System.Reflection.Metadata;

namespace AppQR.Dapper
{
    public class EventosRepositorio : DapperRepo, IEventosRepositorio
    {
        public EventosRepositorio(IDbConnection conexion) : base(conexion) { }


        public Evento AgregarEvento(Evento evento)
        {
            var sql = @"INSERT INTO Evento (Nombre, Estado, FechaInicio, FechaFin, IdLocal) VALUES (@nombre, @estado, @fechaInicio, @fechaFin, @idLocal); 
                SELECT LAST_INSERT_ID();";
            var id = Conexion.ExecuteScalar<int>(sql, new
            {
                nombre = evento.Nombre,
                estado = evento.Estado,
                fechaInicio = evento.FechaInicio,
                fechaFin = evento.FechaFin,
                idLocal = evento.local?.IdLocal
            });
            evento.IdEvento = id;
            return evento;
        }

        public bool ActualizarEvento(Evento evento)
        {
            var sql = @"UPDATE Evento SET Nombre = @nombre, Estado = @estado, FechaInicio = @fechaInicio, FechaFin = @fechaFin, IdLocal = @idLocal
            WHERE IdEvento = @idEvento";
            var rowsAffected = Conexion.Execute(sql, new
            {
                nombre = evento.Nombre,
                estado = evento.Estado,
                fechaInicio = evento.FechaInicio,
                fechaFin = evento.FechaFin,
                idLocal = evento.local?.IdLocal
            });
            return rowsAffected > 0;
        }

        public bool EliminarEvento(int id)
        {
            var sql = @"DELETE FROM Evento WHERE IdEvento = @Id";
            var rowsAffected = Conexion.Execute(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public IEnumerable<Evento> ObtenerEventos()
        {
            var sql = "SELECT * FROM Evento";
            return Conexion.Query<Evento>(sql);
        }

        public Evento ObtenerEventoPorID(int id)
        {
            var sql = "SELECT * FROM Evento WHERE IdEvento = @Id";
            var evento = Conexion.QueryFirstOrDefault<Evento>(sql, new { Id = id });
            return evento;
        }

        public bool CancelarEvento(int id)
        {
            var sql = @"UPDATE Evento SET Estado = 'Cancelado' WHERE IdEvento = @Id";
            var rowsAffected = Conexion.Execute(sql, new { Id = id });
            return rowsAffected > 0;
        }
    }
}