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
            var sql = @"INSERT INTO Eventos (Nombre, Estado, IdLocal) VALUES (@nombre, @estado, @idLocal); 
                SELECT LAST_INSERT_ID();";
            var id = Conexion.ExecuteScalar<int>(sql, new
            {
                nombre = evento.Nombre,
                estado = evento.Estado,
                idLocal = evento.local?.IdLocal
            });
            evento.IdEvento = id;
            return evento;
        }

        public bool ActualizarEvento(Evento evento)
        {
            var sql = @"UPDATE Eventos SET Nombre = @Nombre, Estado = @Estado, IdLocal = @IdLocal
            WHERE IdEvento = @IdEvento";
            var rowsAffected = Conexion.Execute(sql, evento);
            return rowsAffected > 0;
        }

        public bool EliminarEvento(int id)
        {
            var sql = @"DELETE FROM Eventos WHERE IdEvento = @Id";
            var rowsAffected = Conexion.Execute(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public IEnumerable<Evento> ObtenerEventos()
        {
            var sql = "SELECT * FROM Eventos";
            return Conexion.Query<Evento>(sql);
        }

        public Evento ObtenerEventoPorID(int id)
        {
            var sql = "SELECT * FROM Eventos WHERE IdEvento = @Id";
            var evento = Conexion.QueryFirstOrDefault<Evento>(sql, new { Id = id });
            return evento;
        }
    }
}