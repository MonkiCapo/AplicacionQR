using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using AppQR.Core;
using AppQR.Core.Servicios;
using AppQR.Core.Entidades;

namespace AppQR.Dapper
{
    public class EntradaRepositorio : IEntradaRepositorio
    {
        private readonly IAdo _ado;
        public EntradaRepositorio(IAdo ado) => _ado = ado;

        public Entrada AgregarEntrada(Entrada entrada)
        {
            var sql = @"INSERT INTO Entradas (IdTarifa, IdOrden, CodigoQR, Estado)
                VALUES (@IdTarifa, @IdOrden, @CodigoQR, @Estado);
                SELECT LAST_INSERT_ID();";
            using var db = _ado.GetDbConnection();
            var id = db.ExecuteScalar<int>(sql, entrada);
            entrada.IdEntrada = id;
            return entrada;
        }

        public bool ActualizarEntrada(Entrada entrada)
        {
            var sql = @"UPDATE Entradas SET 
                    IdTarifa = @IdTarifa, 
                    IdOrden = @IdOrden, 
                    CodigoQR = @CodigoQR, 
                    Estado = @Estado
                WHERE IdEntrada = @IdEntrada";
            using var db = _ado.GetDbConnection();
            var rowsAffected = db.Execute(sql, entrada);
            return rowsAffected > 0;
        }

        public bool EliminarEntrada(int id)
        {
            var sql = "DELETE FROM Entradas WHERE IdEntrada = @Id";
            using var db = _ado.GetDbConnection();
            var rowsAffected = db.Execute(sql, new { Id = id });
            return rowsAffected > 0;
        }


        public IEnumerable<Entrada> ObtenerEntradas()
        {
            var sql = "SELECT * FROM Entradas";
            using var db = _ado.GetDbConnection();
            return db.Query<Entrada>(sql);
        }

        public Entrada ObtenerEntradaPorID(int id)
        {
            var sql = "SELECT * FROM Entradas WHERE IdEntrada = @Id";
            using var db = _ado.GetDbConnection();
            var entrada = db.QueryFirstOrDefault<Entrada>(sql, new { Id = id });
            if (entrada == null)
                throw new InvalidOperationException($"No se encontró una entrada con el ID {id}.");
            return entrada;
        }
    }
}