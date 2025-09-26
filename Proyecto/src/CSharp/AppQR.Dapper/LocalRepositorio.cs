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

        public Local ObtenerLocalPorID(int id)
        {
            var sql = "SELECT * FROM Local WHERE IdLocal = @Id";
            var local = Conexion.QueryFirstOrDefault<Local>(sql, new { Id = id });
            if (local == null)
                throw new InvalidOperationException($"No se encontró un local con el ID {id}.");
            return local;
        }

        public Sector AgregarSector(Sector sector, int id)
        {
            var sql = @"INSERT INTO Sector (Nombre, Capacidad, IdLocal) 
                        VALUES (@nombre, @capacidad, @idLocal); 
                        SELECT LAST_INSERT_ID();";
            var ID = Conexion.ExecuteScalar<int>(sql, new { nombre = sector.Nombre, capacidad = sector.Capacidad, idLocal = id });
            sector.IdSector = ID;
            return sector;
        }

        public bool ActualizarSector(Sector sector, int id)
        {
            var sql = @"UPDATE Sector SET Nombre = @nombre, Capacidad = @capacidad, IdLocal = @idLocal
                        WHERE IdSector = @idSector";
            var rowsAffected = Conexion.Execute(sql, new { idSector = sector.IdSector, nombre = sector.Nombre, capacidad = sector.Capacidad, idLocal = id });
            return rowsAffected > 0;
        }

        public bool EliminarSector(int idSECTOR)
        {
            var sql = @"DELETE FROM Sector WHERE IdSector = @idSector";
            var rowsAffected = Conexion.Execute(sql, new { IdSector = idSECTOR });
            return rowsAffected > 0;
        }

        public IEnumerable<Sector> ObtenerSectoresPorLocal(int idLocal)
        {
            var sql = "SELECT * FROM Sector WHERE IdLocal = @idLocal";
            return Conexion.Query<Sector>(sql, new { IdLocal = idLocal });
        }

        public Sector ObtenerSectorPorID(int idSECTOR)
        {
            var sql = "SELECT * FROM Sector WHERE IdSector = @idSector";
            var sector = Conexion.QueryFirstOrDefault<Sector>(sql, new { IdSector = idSECTOR });
            return sector;
        }
    }
}