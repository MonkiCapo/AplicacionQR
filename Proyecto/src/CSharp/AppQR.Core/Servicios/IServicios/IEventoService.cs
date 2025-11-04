using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Entidades;
using AppQR.Core.Dto;

namespace AppQR.Core.Servicios.IServicios
{
    public interface IEventoService
    {
        IEnumerable<Evento> ObtenerEventos();
        Evento ObtenerEventoPorID(int id);
        Evento AgregarEvento(EventoDTO evento);
        bool ActualizarEvento(EventoDTO evento, int id);
        bool EliminarEvento(int id);

        string CancelarEvento(int id);
        string PublicarEvento(int id);

    }
}