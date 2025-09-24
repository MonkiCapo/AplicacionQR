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

        IEnumerable<Sector> ObtenerSectoresPorLocal(int idLocal);
        Sector ObtenerSectorPorID(int idSector);
        Sector AgregarSector(Sector sector, int idLocal);
        bool ActualizarSector(Sector sector, int idLocal);
        bool EliminarSector(int idSector);
    }
}