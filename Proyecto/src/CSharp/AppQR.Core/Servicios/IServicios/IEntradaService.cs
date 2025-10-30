using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Entidades;

namespace AppQR.Core.Servicios.IServicios
{
    public interface IEntradaService
    {
        IEnumerable<Entrada> ObtenerEntradas();
        Entrada ObtenerEntradaPorID(int id);
    }
}