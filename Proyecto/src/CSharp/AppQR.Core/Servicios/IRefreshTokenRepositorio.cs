using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Entidades;

namespace AppQR.Core.Servicios
{
    public interface IRefreshTokenRepositorio
    {
        public int InsertarToken(RefreshToken token);
        public RefreshToken? ObtenerToken(string token);
        public void EliminarToken(string token);
        public void EliminarTokensPorEmail(string email);
        public void ReemplazarToken(int IdUsuario, string nuevoHash, DateTime expiracion);
    }
}