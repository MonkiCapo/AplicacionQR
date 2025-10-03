using System.Data;
using MySqlConnector;

namespace AppQR.Test;

public class TestAdo
{
    public IDbConnection Conexion { get; set; }

    public TestAdo()
    {
        string conexionCadena = "Server=localhost;Database=AppQR;Uid=root;Pwd=root;";
        Conexion = new MySqlConnection(conexionCadena);
    }
}

