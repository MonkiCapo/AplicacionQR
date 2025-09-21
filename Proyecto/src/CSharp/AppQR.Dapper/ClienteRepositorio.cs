using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using AppQR.Core;
using AppQR.Core.Servicios;



namespace AppQR.Dapper
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly IAdo _ado;
        public ClienteRepositorio(IAdo ado) => _ado = ado;

        public async Task<Cliente> AgregarCliente(Cliente cliente)
        {
            var sql = @"INSERT INTO Clientes (Nombre, Contraseña, Email, Telefono, dni) 
                VALUES (@Nombre, @Contraseña, @Email, @Telefono, @dni); 
                SELECT LAST_INSERT_ID();";
            using var db = _ado.GetDbConnection();
            var id = await db.ExecuteScalarAsync<int>(sql, cliente);
            cliente.IdCliente = id;
            return cliente;
        }

        public async Task<bool> ActualizarCliente(Cliente cliente)
        {
            var sql = @"UPDATE Clientes SET Nombre = @Nombre, Contraseña = @Contraseña, 
                Email = @Email, Telefono = @Telefono, dni = @dni WHERE IdCliente = @IdCliente";
            using var db = _ado.GetDbConnection();
            var rowsAffected = await db.ExecuteAsync(sql, cliente);
            return rowsAffected > 0;
        }

        public async Task<bool> EliminarCliente(int id)
        {
            var sql = "DELETE FROM Clientes WHERE IdCliente = @Id";
            using var db = _ado.GetDbConnection();
            var rowsAffected = await db.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<Cliente> ObtenerClientePorID(int id)
        {
            var sql = "SELECT * FROM Clientes WHERE IdCliente = @Id";
            using var db = _ado.GetDbConnection();
            var cliente = await db.QueryFirstOrDefaultAsync<Cliente>(sql, new { Id = id });
            if (cliente == null)
                throw new InvalidOperationException($"No se encontró un cliente con el ID {id}.");
            return cliente;
        }

        public async Task<IEnumerable<Cliente>> ObtenerClientes()
        {
            var sql = "SELECT * FROM Clientes";
            using var db = _ado.GetDbConnection();
            return await db.QueryAsync<Cliente>(sql);
        }
    }
}