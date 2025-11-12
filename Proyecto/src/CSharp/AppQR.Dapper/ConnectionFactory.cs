using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace AppQR.Dapper
{
    public class ConnectionFactory
    {
        private readonly IConfiguration _config;

        public ConnectionFactory(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection CrearConexionPorRol(string rol)
        {
            string? connectionString = rol switch
            {
                "Admin" => _config.GetConnectionString("AdminConnection"),
                "Organizador" => _config.GetConnectionString("OrganizadorConnection"),
                "Cliente" => _config.GetConnectionString("ClienteConnection"),
                _ => throw new Exception($"Rol desconocido: {rol}")
            };

            return new MySqlConnection(connectionString);
        }
    }
}