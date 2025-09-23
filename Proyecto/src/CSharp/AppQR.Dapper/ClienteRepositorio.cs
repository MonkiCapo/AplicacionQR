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
            var sql = @"INSERT INTO Clientes (Nombre, Telefono) 
                        VALUES (@Nombre, @Telefono);
                        SELECT LAST_INSERT_ID();";
            var id = Conexion.ExecuteScalar<int>(sql, cliente);
            cliente.DNI = id;
            return cliente;
        }

        public bool ActualizarCliente(Cliente cliente)
        {
            var sql = @"UPDATE Clientes SET 
                            Nombre = @Nombre, 
                            Telefono = @Telefono
                        WHERE DNI = @DNI";
            var rowsAffected = Conexion.Execute(sql, cliente);
            return rowsAffected > 0;
        }

        public bool EliminarCliente(int dni)
        {
            var sql = "DELETE FROM Clientes WHERE DNI = @DNI";
            var rowsAffected = Conexion.Execute(sql, new { DNI = dni });
            return rowsAffected > 0;
        }

        public IEnumerable<Cliente> ObtenerClientes()
        {
            var sql = "SELECT DNI, Nombre, Telefono FROM Clientes";
            return Conexion.Query<Cliente>(sql);
        }

        public Cliente ObtenerClientePorID(int dni)
        {
            var sql = "SELECT DNI, Nombre, Telefono FROM Clientes WHERE DNI = @DNI";
            var cliente = Conexion.QueryFirstOrDefault<Cliente>(sql, new { DNI = dni });
            if (cliente == null)
                throw new InvalidOperationException($"No se encontró un cliente con el DNI {dni}.");
            return cliente;
        }
    }
}
