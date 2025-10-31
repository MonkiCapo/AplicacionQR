using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Dto;
using AppQR.Core.Entidades;

namespace AppQR.Core.Servicios.IServicios
{
    public interface IEntradaService
    {
        IEnumerable<Entrada> ObtenerEntradas();
        Entrada ObtenerEntradaPorID(int id);
        Entrada AgregarEntrada(EntradaDTO entradaDTO);
        bool ActualizarEntrada(EntradaDTO entradaDTO, int id);
        bool EliminarEntrada(int id);
    }
}