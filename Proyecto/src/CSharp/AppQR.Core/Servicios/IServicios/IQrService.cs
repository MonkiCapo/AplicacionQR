using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core.Servicios.IServicios
{
    public interface IQrService
    {
        string GenerarUrldeQR(int idEntrada);
        byte[] CrearQR(string Url);
    }
}