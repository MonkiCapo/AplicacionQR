using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core.Servicios
{
    public interface IEventosRepositorio
    {
        Task<IEnumerable<Evento>> ObtenerEventos();
        Task<Evento> ObtenerEventoPorID(int id);
        Task<Evento> AgregarEvento(Evento evento);
        Task<bool> ActualizarEvento(Evento evento);
        Task<bool> EliminarEvento(int id);
    }
}