using System.Data;
using AppQR.Core.Servicios.IServicios;
using AppQR.Core.Servicios.Repositorios;

namespace AppQR.Dapper
{
    public class Ado : IAdo
    {
        private readonly string Conexion;
        public Ado(IDataBaseConnectionService _service)
        {
            
        }
    }
}