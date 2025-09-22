using System.Data;
using System.Data.Common;

namespace AppQR.Dapper;
public abstract class DapperRepo
{
    protected IDbConnection Conexion { get; set; }

    protected DapperRepo(IDbConnection conexion) => Conexion = conexion;
}