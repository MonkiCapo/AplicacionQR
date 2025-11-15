using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using AppQR.Core.Servicios.IServicios;
using System.Data;
using MySql.Data.MySqlClient;

namespace AppQR.Services.Servicios
{
    public class DataBaseConnectionService : IDataBaseConnectionService
    {
        public string GetConnectionRootString()
        {
            var configuration = LeerJson();

            var connectionStrings = configuration.GetSection("Root").GetChildren();

            string? connectionString = ProbarCadenas(connectionStrings);

            if (connectionString == null)
            {
                throw new ArgumentException("Ninguna cadena de conexión root funcionó.");
            }

            return connectionString;
        }

        public string GetConnectionUserString(string rol)
        {
            var configuration = LeerJson();

            var connectionStrings = configuration.GetSection("Users").GetChildren();

            return connectionStrings.First(c => c.Key == rol).Value!;
        }

        static IConfigurationRoot LeerJson()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(
                    "appsettings.json",
                    optional: false
                    )
                .Build();
            return configuration;
        }
        static bool ProbarConexion(string cs)
        {
            try
            {
                using var conn = new MySqlConnection(cs);
                conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        static string RemoverBaseDeDatosDeLaCadena(string cs)
        {
            var builder = new MySqlConnectionStringBuilder(cs);
            builder.Remove("Database");
            return builder.ConnectionString;
        }

        static string ProbarCadenas(IEnumerable<IConfigurationSection>? connectionStrings)
        {
            if (connectionStrings != null)
            {
                foreach (var cs in connectionStrings)
                {
                    var connection = RemoverBaseDeDatosDeLaCadena(cs.Value);
                    if (ProbarConexion(connection))
                    {
                        return cs.Value;
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }
    }
}
