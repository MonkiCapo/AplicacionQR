using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Dto;
using AppQR.Core.Entidades;

namespace AppQR.Core.Servicios.IServicios
{
    public interface IOrdenService
    {
        IEnumerable<Orden> ObtenerOrdenes();
        Orden ObtenerOrdenPorID(int id);
        Orden AgregarOrden(OrdenDTO ordenDto);
        string CancelarOrden(int id);
        string PagarOrden(int id, EntradaDTO entradaDto);
    }
}