using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core.Servicios
{
    public interface IEntradaRepositorio
    {
        IEnumerable<Entrada> ObtenerEntradas();
        Entrada ObtenerEntradaPorID(int id);
        Entrada AgregarEntrada(Entrada entrada);
        bool ActualizarEntrada(Entrada entrada);
        bool EliminarEntrada(int id);
    }
}