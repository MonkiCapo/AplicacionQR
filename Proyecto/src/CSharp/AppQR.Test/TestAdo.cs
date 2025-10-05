using System.Data;
using MySqlConnector;

namespace AppQR.Test;

public class TestAdo
{
    public IDbConnection Conexion { get; set; }

    public TestAdo()
    {
        string conexionCadena = "Server=localhost;Database=AppQR;User=root;Password=corrientes;";
        Conexion = new MySqlConnection(conexionCadena);
    }
}

