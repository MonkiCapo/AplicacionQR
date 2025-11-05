using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Entidades;

namespace AppQR.Core.Servicios.Repositorios
{
    public interface IQrRepositorio
    {
        QR? ObtenerQr(int idQR);
        QR AltaQR(QR qr);
    }
}