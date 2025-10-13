using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios;
using AppQR.Core.Servicios.Enums;

namespace AppQR.Dapper
{
    public class EntradaRepositorio : DapperRepo, IEntradaRepositorio
    {
        public EntradaRepositorio(IDbConnection conexion) : base(conexion) { }

         public Entrada AgregarEntrada(Entrada entrada)
        {
            var sql = @"
                INSERT INTO Entrada (IdTarifa, IdOrden, Estado)
                VALUES (@idTarifa, @idOrden, @estado);
                SELECT LAST_INSERT_ID();";

            var id = Conexion.ExecuteScalar<int>(sql, new
            {
                idTarifa = entrada.tarifa?.IdTarifa,
                idOrden = entrada.orden?.IdOrden,
                estado = entrada.Estado.ToString()
            });

            entrada.IdEntrada = id;
            return entrada;
        }

        public bool ActualizarEntrada(Entrada entrada)
        {
            var sql = @"
                UPDATE Entrada SET 
                    IdTarifa = @idTarifa, 
                    IdOrden = @idOrden, 
                    Estado = @estado
                WHERE IdEntrada = @idEntrada";

            var rowsAffected = Conexion.Execute(sql, new
            {
                idEntrada = entrada.IdEntrada,
                idTarifa = entrada.tarifa?.IdTarifa,
                idOrden = entrada.orden?.IdOrden,
                estado = entrada.Estado.ToString()
            });

            return rowsAffected > 0;
        }

        public bool EliminarEntrada(int id)
        {
            var sql = "DELETE FROM Entrada WHERE IdEntrada = @Id";
            var rowsAffected = Conexion.Execute(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public IEnumerable<Entrada> ObtenerEntradas()
        {
            var sql = @"SELECT 
                    e.IdEntrada,
                    e.Estado AS EstadoEntrada,

                    t.IdTarifa,
                    t.Tipo AS TipoTarifa,
                    t.Precio,
                    t.Estado AS EstadoTarifa,
                    t.IdFuncion,

                    o.IdOrden,
                    o.Estado AS EstadoOrden,
                    o.PrecioTotal,
                    o.Fecha
                FROM Entrada e
                INNER JOIN Tarifa t ON e.IdTarifa = t.IdTarifa
                INNER JOIN Orden o ON e.IdOrden = o.IdOrden;";

            var entradas = Conexion.Query<Entrada, Tarifa, Orden, Entrada>(
                sql,
                (entrada, tarifa, orden) =>
                {
                    entrada.tarifa = tarifa;
                    entrada.orden = orden;
                    return entrada;
                },
                splitOn: "IdTarifa,IdOrden" 
            );

            return entradas;
        }

        public Entrada ObtenerEntradaPorID(int id)
        {
            var sql = @"SELECT 
                    e.IdEntrada,
                    e.Estado AS EstadoEntrada,

                    t.IdTarifa,
                    t.Tipo AS TipoTarifa,
                    t.Precio,
                    t.Estado AS EstadoTarifa,
                    t.IdFuncion,

                    o.IdOrden,
                    o.Estado AS EstadoOrden,
                    o.PrecioTotal,
                    o.Fecha
                FROM Entrada e
                INNER JOIN Tarifa t ON e.IdTarifa = t.IdTarifa
                INNER JOIN Orden o ON e.IdOrden = o.IdOrden
                WHERE e.IdEntrada = @Id;";

            var entrada = Conexion.Query<Entrada, Tarifa, Orden, Entrada>(
                sql,
                (entrada, tarifa, orden) =>
                {
                    entrada.tarifa = tarifa;
                    entrada.orden = orden;
                    return entrada;
                },
                new { Id = id },
                splitOn: "IdTarifa,IdOrden"
            ).FirstOrDefault();

            return entrada;
        }
    }
}
