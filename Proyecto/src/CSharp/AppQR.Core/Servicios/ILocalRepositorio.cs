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
        Local ObtenerLocalPorID(int id);
        Local AgregarLocal(Local local);
        bool ActualizarLocal(Local local);
        bool EliminarLocal(int id);

        IEnumerable<Sector> ObtenerSectoresPorLocal(int idLocal);
        Sector ObtenerSectorPorID(int idSECTOR);
        Sector AgregarSector(Sector sector, int id);
        bool ActualizarSector(Sector sector, int id);
        bool EliminarSector(int idSECTOR);
    }
}