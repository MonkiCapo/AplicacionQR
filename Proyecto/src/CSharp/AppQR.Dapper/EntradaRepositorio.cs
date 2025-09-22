using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios;

namespace AppQR.Dapper
{
    public class EntradaRepositorio : DapperRepo, IEntradaRepositorio
    {
        public EntradaRepositorio(IDbConnection conexion) : base(conexion) { }

        public Entrada AgregarEntrada(Entrada entrada)
        {
            var sql = @"INSERT INTO Entradas (IdTarifa, IdOrden, CodigoQR, Estado)
                        VALUES (@IdTarifa, @IdOrden, @CodigoQR, @Estado);
                        SELECT LAST_INSERT_ID();";
            var id = Conexion.ExecuteScalar<int>(sql, entrada);
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
            var rowsAffected = Conexion.Execute(sql, entrada);
            return rowsAffected > 0;
        }

        public bool EliminarEntrada(int id)
        {
            var sql = "DELETE FROM Entradas WHERE IdEntrada = @Id";
            var rowsAffected = Conexion.Execute(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public IEnumerable<Entrada> ObtenerEntradas()
        {
            var sql = "SELECT * FROM Entradas";
            return Conexion.Query<Entrada>(sql);
        }

        public Entrada ObtenerEntradaPorID(int id)
        {
            var sql = "SELECT * FROM Entradas WHERE IdEntrada = @Id";
            var entrada = Conexion.QueryFirstOrDefault<Entrada>(sql, new { Id = id });
            if (entrada == null)
                throw new InvalidOperationException($"No se encontró una entrada con el ID {id}.");
            return entrada;
        }
    }
}
