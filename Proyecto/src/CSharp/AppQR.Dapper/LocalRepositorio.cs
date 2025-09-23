using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using AppQR.Core;
using AppQR.Core.Servicios;
using AppQR.Core.Entidades;

namespace AppQR.Dapper
{
    public class LocalRepositorio : DapperRepo, ILocalRepositorio
    {
        public LocalRepositorio(IDbConnection conexion) : base(conexion) { }

        public Local AgregarLocal(Local local)
        {
            var sql = @"INSERT INTO Local (Nombre, Dirección) VALUES (@Nombre, @Direccion); 
                SELECT LAST_INSERT_ID();";
            var id = Conexion.ExecuteScalar<int>(sql, local);
            local.IdLocal = id;
            return local;
        }

        public bool ActualizarLocal(Local local)
        {
            var sql = @"UPDATE Local SET Nombre = @Nombre, Direccion = @Direccion
            WHERE IdLocal = @IdLocal";
            var rowsAffected = Conexion.Execute(sql, local);
            return rowsAffected > 0;
        }

        public bool EliminarLocal(int id)
        {
            var sql = @"DELETE FROM Local WHERE IdLocal = @Id";
            var rowsAffected = Conexion.Execute(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public IEnumerable<Local> ObtenerLocales()
        {
            var sql = "SELECT * FROM Local";
            return Conexion.Query<Local>(sql);
        }

        public Local ObtenerPorID(int id)
        {
            var sql = "SELECT * FROM Local WHERE IdLocal = @Id";
            var local = Conexion.QueryFirstOrDefault<Local>(sql, new { Id = id });
            if (local == null)
                throw new InvalidOperationException($"No se encontró un local con el ID {id}.");
            return local;
        }
    }
}