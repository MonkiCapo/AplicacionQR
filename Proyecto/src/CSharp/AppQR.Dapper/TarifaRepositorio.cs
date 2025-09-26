using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using AppQR.Core;
using AppQR.Core.Servicios;
using AppQR.Core.Entidades;

namespace AppQR.Dapper
{
    public class TarifaRepositorio : DapperRepo, ITarifaRepositorio
    {
        public TarifaRepositorio(IDbConnection conexion) : base(conexion) { }

        public Tarifa AgregarTarifa(Tarifa tarifa)
        {
            var sql = @"INSERT INTO Tarifa (Precio, Stock, Estado, IdFuncion) 
                VALUES (@precio, @stock, @estado, @idFuncion);
                SELECT LAST_INSERT_ID();";
            var id = Conexion.ExecuteScalar<int>(sql, new
            {
                precio = tarifa.Precio,
                stock = tarifa.Stock,
                estado = tarifa.Estado,
                idFuncion = tarifa.funcion.IdFuncion
            });
            tarifa.IdTarifa = id;
            return tarifa;
        }

        public bool ActualizarTarifa(Tarifa tarifa)
        {
            var sql = @"UPDATE Tarifa SET 
                            Precio = @precio, 
                            Stock = @stock, 
                            Estado = @estado, 
                            IdFuncion = @idFuncion
                        WHERE IdTarifa = @idTarifa";
            var rowsAffected = Conexion.Execute(sql, new
            {
                precio = tarifa.Precio,
                stock = tarifa.Stock,
                estado = tarifa.Estado,
                idFuncion = tarifa.funcion.IdFuncion,
                idTarifa = tarifa.IdTarifa
            });
            return rowsAffected > 0;
        }

        public bool EliminarTarifa(int id)
        {
            var sql = @"DELETE FROM Tarifa WHERE IdTarifa = @Id";
            var rowsAffected = Conexion.Execute(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public IEnumerable<Tarifa> ObtenerTodasLasTarifas()
        {
            var sql = "SELECT * FROM Tarifa";
            return Conexion.Query<Tarifa>(sql);
        }

        public Tarifa ObtenerTarifaPorID(int id)
        {
            var sql = "SELECT * FROM Tarifa WHERE IdTarifa = @Id";
            var tarifa = Conexion.QueryFirstOrDefault<Tarifa>(sql, new { Id = id });
            return tarifa;
        }
    }
}