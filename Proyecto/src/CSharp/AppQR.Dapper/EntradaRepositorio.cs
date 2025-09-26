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
                VALUES (@idTarifa, @idOrden, @codigoQR, @estado);
                SELECT LAST_INSERT_ID();";
            var id = Conexion.ExecuteScalar<int>(sql, new
            {
                idTarifa = entrada.tarifa?.IdTarifa,
                idOrden = entrada.orden?.IdOrden,
                codigoQR = entrada.CodigoQR,
                estado = entrada.Estado
            });
            entrada.IdEntrada = id;
            return entrada;
        }

        public bool ActualizarEntrada(Entrada entrada)
        {
            var sql = @"UPDATE Entradas SET 
                            IdTarifa = @idTarifa, 
                            IdOrden = @idOrden, 
                            CodigoQR = @codigoQR, 
                            Estado = @estado
                        WHERE IdEntrada = @idEntrada";
            var rowsAffected = Conexion.Execute(sql, new
            {
                idTarifa = entrada.tarifa?.IdTarifa,
                idOrden = entrada.orden?.IdOrden,
                codigoQR = entrada.CodigoQR,
                estado = entrada.Estado,
                idEntrada = entrada.IdEntrada
            });
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
            return entrada;
        }
    }
}
