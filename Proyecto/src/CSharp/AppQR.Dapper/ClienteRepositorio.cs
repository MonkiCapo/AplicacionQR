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
                        VALUES (@dni, @nombre, @telefono);";

            Conexion.Execute(sql, new
            {
                dni = cliente.DNI,
                nombre = cliente.Nombre,
                telefono = cliente.Telefono
            });
            return cliente;
        }

        public bool ActualizarCliente(Cliente cliente)
        {
            var sql = @"UPDATE Cliente SET 
                        Nombre = @nombre, 
                        Telefono = @telefono
                        WHERE DNI = @dni;";
            var rowsAffected = Conexion.Execute(sql, new
            {
                nombre = cliente.Nombre,
                telefono = cliente.Telefono
            });
            return rowsAffected > 0;
        }

        public bool EliminarCliente(int dni)
        {
            var sql = "DELETE FROM Cliente WHERE DNI = @Dni;";
            var rowsAffected = Conexion.Execute(sql, new { Dni = dni });
            return rowsAffected > 0;
        }

        public IEnumerable<Cliente> ObtenerClientes()
        {
            var sql = "SELECT DNI, Nombre, Telefono FROM Cliente;";
            return Conexion.Query<Cliente>(sql);
        }

        public Cliente ObtenerClientePorDNI(int dni)
        {
            var sql = "SELECT DNI, Nombre, Telefono FROM Cliente WHERE DNI = @DNI;";
            var cliente = Conexion.QueryFirstOrDefault<Cliente>(sql, new { DNI = dni });
            return cliente;
        }

        public bool ExisteDNIdeCliente(int dniExistente)
        {
            var sql = Conexion.QueryFirstOrDefault("SELECT COUNT(1) FROM Cliente WHERE DNI = @Dni LIMIT 1;", new { Dni = dniExistente });
            if (sql = null)
            {
                return false;
            }

            return true;
        }

    }
}
