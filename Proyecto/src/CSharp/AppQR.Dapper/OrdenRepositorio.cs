using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios;
using AppQR.Core.Servicios.Enums;

namespace AppQR.Dapper
{
    public class OrdenRepositorio : DapperRepo, IOrdenRepositorio
    {
        public OrdenRepositorio(IDbConnection conexion) : base(conexion) { }

        public Orden AgregarOrden(Orden orden)
        {
            var sql = @"INSERT INTO Orden(IdUsuario, Estado, PrecioTotal, Fecha) VALUES (@idusuario, @estado, @precioTotal, @fecha);
            SELECT LAST_INSERT_ID();";

            var id = Conexion.ExecuteScalar<int>(sql, new
            {
                idusuario = orden.usuario.IdUsuario,
                estado = orden.Estado.ToString(),
                precioTotal = orden.PrecioTotal,
                fecha = orden.Fecha
            });
            orden.IdOrden = id;
            return orden;

        }

        public IEnumerable<Orden> ObtenerOrdenes()
        {
            var sql = @"SELECT o.*, u.IdUsuario, u.NombreUsuario, u.DNI
                        FROM Orden o
                        INNER JOIN Usuario u ON o.IdUsuario = u.IdUsuario";

            var ordenes = Conexion.Query<Orden, Usuario, Orden>(sql,
                (orden, usuario) =>
                {
                    orden.usuario = usuario;
                    return orden;
                },
                splitOn: "IdUsuario"
            );

            return ordenes;
        }

        public Orden ObtenerOrdenPorID(int id)
        {
            var sql = @"SELECT o.*, u.IdUsuario, u.NombreUsuario, u.DNI
                        FROM Orden o
                        INNER JOIN Usuario u ON o.IdUsuario = u.IdUsuario
                        WHERE o.IdOrden = @Id";

            var orden = Conexion.Query<Orden, Usuario, Orden>(sql, (o, u) =>
            {
                o.usuario = u;
                return o;
            },
            new { Id = id },
            splitOn: "IdUsuario").SingleOrDefault();

            return orden;
        }

        public string PagarOrden(int id)
        {
            var sql = "CALL PagarOrden(@id)";
            var mensaje = Conexion.QueryFirstOrDefault<string>(sql, new { id });
            return mensaje ?? "No se pudo pagar la orden";
        }

        public string CancelarOrden(int id)
        {
            var sql = "CALL CancelarOrden(@id)";
            var mensaje = Conexion.QueryFirstOrDefault<string>(sql, new { id });
            return mensaje ?? "No se pudo cancelar la orden";
        }
    }
}