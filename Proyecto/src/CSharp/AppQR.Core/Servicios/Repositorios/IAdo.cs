using System.Data;

namespace AppQR.Core.Servicios.Repositorios
{
    public interface IAdo
    {
         IDbConnection GetDbConnection();
    }
}