using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Entidades;

namespace AppQR.Core.Servicios
{
    public interface IOrdenRepositorio
    {
        IEnumerable<Orden> ObtenerOrdenes();
        Orden ObtenerOrdenPorID(int id);
        Orden AgregarOrden(Orden orden);
        string CancelarOrden(int id);
        string OrdenPagada(int id);
    }
}