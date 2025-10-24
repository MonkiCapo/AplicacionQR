using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Dto;
using AppQR.Core.Entidades;

namespace AppQR.Core.Servicios.IServicios
{
    public interface ILocalService
    {
        IEnumerable<Local> ObtenerLocales();
        Local ObtenerLocalPorID(int id);
        Local AgregarLocal(LocalDTO local);
        bool ActualizarLocal(LocalDTO local, int id);
        bool EliminarLocal(int id);

        IEnumerable<Sector> ObtenerSectoresPorLocal(int idLocal);
        Sector ObtenerSectorPorID(int id);
        Sector AgregarSector(SectorDTO sector, int id);
        bool ActualizarSector(SectorDTO sector, int id);
        bool EliminarSector(int id);
    }
}