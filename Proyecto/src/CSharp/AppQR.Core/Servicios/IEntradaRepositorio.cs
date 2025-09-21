using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core.Servicios
{
    public interface IEntradaRepositorio
    {
        Task<IEnumerable<Entrada>> ObtenerEntradas();
        Task<Entrada> ObtenerEntradaPorID(int id);
        Task<Entrada> AgregarEntrada(Entrada entrada);
        Task<bool> ActualizarEntrada(Entrada entrada);
        Task<bool> EliminarEntrada(int id);
    }
}