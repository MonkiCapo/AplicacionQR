using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Entidades;

namespace AppQR.Core.Servicios
{
    public interface IEventosRepositorio
    {
        IEnumerable<Evento> ObtenerEventos();
        Evento ObtenerEventoPorID(int id);
        Evento AgregarEvento(Evento evento);
        bool ActualizarEvento(Evento evento);
        bool EliminarEvento(int id);
    }
}