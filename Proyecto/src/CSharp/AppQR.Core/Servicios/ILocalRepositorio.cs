using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Entidades;

namespace AppQR.Core.Servicios
{
    public interface ILocalRepositorio
    {
        IEnumerable<Local> ObtenerLocales();
        Local ObtenerPorID(int id);
        Local AgregarLocal(Local local);
        bool ActualizarLocal(Local local);
        bool EliminarLocal(int id);
    }
}