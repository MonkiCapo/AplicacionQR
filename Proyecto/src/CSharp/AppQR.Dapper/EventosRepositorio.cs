using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using AppQR.Core;
using AppQR.Core.Servicios;
using System.Reflection.Metadata;

namespace AppQR.Dapper
{
    public class EventosRepositorio : IEventosRepositorio
    {
        public readonly IAdo _ado;
        public EventosRepositorio(IAdo ado) => _ado = ado;


        public Evento AgregarEvento(Evento evento)
        {
            var sql = @"INSERT INTO Eventos (Nombre, Estado, IdLocal) VALUES (@Nombre, @Estado, @IdLocal); 
                SELECT LAST_INSERT_ID();";
            using var db = _ado.GetDbConnection();
            var id = db.ExecuteScalar<int>(sql, evento);
            evento.IdEvento = id;
            return evento;
        }

        public bool ActualizarEvento(Evento evento)
        {
            var sql = @"UPDATE Eventos SET Nombre = @Nombre, Estado = @Estado, IdLocal = @IdLocal
            WHERE IdEvento = @IdEvento";
            using var db = _ado.GetDbConnection();
            var rowsAffected = db.Execute(sql, evento);
            return rowsAffected > 0;
        }

        public bool EliminarEvento(int id)
        {
            var sql = "DELETE FROM Eventos WHERE IdEvento = @Id";
            using var db = _ado.GetDbConnection();
            var rowsAffected = db.Execute(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public IEnumerable<Evento> ObtenerEventos()
        {
            var sql = "SELECT * FROM Eventos";
            using var db = _ado.GetDbConnection();
            return db.Query<Evento>(sql);
        }

        public Evento ObtenerEventoPorID(int id)
        {
            var sql = "SELECT * FROM Eventos WHERE IdEvento = @Id";
            using var db = _ado.GetDbConnection();
            var evento = db.QueryFirstOrDefault<Evento>(sql, new { Id = id });
            if (evento == null)
                throw new InvalidOperationException($"No se encontró un evento con el ID {id}.");
            return evento;
        }
    }
}