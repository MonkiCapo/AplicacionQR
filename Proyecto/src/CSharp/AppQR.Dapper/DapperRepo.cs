using System.Data;
using System.Data.Common;
using AppQR.Core.Servicios.Repositorios;

namespace AppQR.Dapper;
public abstract class DapperRepo
{
    protected IDbConnection Conexion { get; set; }

    protected DapperRepo(IAdo _ado) => Conexion = _ado.GetDbConnection();
}