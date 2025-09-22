using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using AppQR.Core;
using AppQR.Core.Servicios;
using AppQR.Core.Entidades;



namespace AppQR.Dapper
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly IAdo _ado;
        public ClienteRepositorio(IAdo ado) => _ado = ado;

        public Cliente AgregarCliente(Cliente cliente)
        {
            var sql = @"INSERT INTO Clientes (Nombre, Contraseña, Email, Telefono, dni) 
                VALUES (@Nombre, @Contraseña, @Email, @Telefono, @dni); 
                SELECT LAST_INSERT_ID();";
            using var db = _ado.GetDbConnection();
            var id = db.ExecuteScalar<int>(sql, cliente);
            cliente.IdCliente = id;
            return cliente;
        }

        public bool ActualizarCliente(Cliente cliente)
        {
            var sql = @"UPDATE Clientes SET Nombre = @Nombre, Contraseña = @Contraseña, 
                Email = @Email, Telefono = @Telefono, dni = @dni WHERE IdCliente = @IdCliente";
            using var db = _ado.GetDbConnection();
            var rowsAffected = db.Execute(sql, cliente);
            return rowsAffected > 0;
        }

        public bool EliminarCliente(int id)
        {
            var sql = "DELETE FROM Clientes WHERE IdCliente = @Id";
            using var db = _ado.GetDbConnection();
            var rowsAffected = db.Execute(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public Cliente ObtenerClientePorID(int id)
        {
            var sql = "SELECT * FROM Clientes WHERE IdCliente = @Id";
            using var db = _ado.GetDbConnection();
            var cliente = db.QueryFirstOrDefault<Cliente>(sql, new { Id = id });
            if (cliente == null)
                throw new InvalidOperationException($"No se encontró un cliente con el ID {id}.");
            return cliente;
        }

        public IEnumerable<Cliente> ObtenerClientes()
        {
            var sql = "SELECT * FROM Clientes";
            using var db = _ado.GetDbConnection();
            return db.Query<Cliente>(sql);
        }
    }
}