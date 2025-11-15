using System.Data;
using AppQR.Core.Servicios.IServicios;
using AppQR.Core.Servicios.Repositorios;
using MySql.Data.MySqlClient;

namespace AppQR.Dapper
{
    public class Ado : IAdo
    {
        private readonly string Conexion;
        public Ado(IDataBaseConnectionService _service, IObtenerRolActualService _RolService) => Conexion = _service.GetConnectionUserString(_RolService.GetRolActual());
        
        public IDbConnection GetDbConnection()
        {
            return new MySqlConnection(Conexion);
        }
    }
}