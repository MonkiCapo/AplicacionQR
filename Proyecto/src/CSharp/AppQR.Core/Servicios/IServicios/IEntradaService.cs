using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Entidades;
using AppQR.Core.Dto;

namespace AppQR.Core.Servicios.IServicios
{
    public interface IEntradaService
    {
        IEnumerable<Entrada> ObtenerEntradas();
        Entrada ObtenerEntradaPorID(int id);
        string AnularEntrada(int id);
        byte[]? ObtenerQR(int id);
        object ValidarQR(string token);
    }
}