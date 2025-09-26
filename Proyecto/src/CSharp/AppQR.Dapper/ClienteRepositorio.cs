using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios;

namespace AppQR.Dapper
{
    public class ClienteRepositorio : DapperRepo, IClienteRepositorio
    {
        public ClienteRepositorio(IDbConnection conexion) : base(conexion) { }

       public Cliente AgregarCliente(Cliente cliente)
{
        var sql = @"INSERT INTO Cliente (DNI, Nombre, Telefono) 
                    VALUES (@dni, @nombre, @telefono);
                    SELECT @dni;";
        
        var Dni = Conexion.ExecuteScalar<int>(sql, new
        {
            dni = cliente.DNI,
            nombre = cliente.Nombre,
            telefono = cliente.Telefono
        });

        cliente.DNI = Dni;
        return cliente;
}
        public bool ActualizarCliente(Cliente cliente)
        {
            var sql = @"UPDATE Cliente SET 
                            Nombre = @Nombre, 
                            Telefono = @Telefono
                        WHERE DNI = @dni";
            var rowsAffected = Conexion.Execute(sql, new
            {
                
            });
            return rowsAffected > 0;
        }

        public bool EliminarCliente(int dni)
        {
            var sql = "DELETE FROM Cliente WHERE DNI = @DNI";
            var rowsAffected = Conexion.Execute(sql, new { DNI = dni });
            return rowsAffected > 0;
        }

        public IEnumerable<Cliente> ObtenerClientes()
        {
            var sql = "SELECT DNI, Nombre, Telefono FROM Cliente";
            return Conexion.Query<Cliente>(sql);
        }

        public Cliente ObtenerClientePorID(int dni)
        {
            var sql = "SELECT DNI, Nombre, Telefono FROM Cliente WHERE DNI = @DNI";
            var cliente = Conexion.QueryFirstOrDefault<Cliente>(sql, new { DNI = dni });
            return cliente;
        }
    }
}
