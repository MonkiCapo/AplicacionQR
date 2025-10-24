using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Dto;
using AppQR.Core.Entidades;

namespace AppQR.Core.Servicios.IServicios
{
    public interface IFuncionService
    {
        IEnumerable<Funcion> ObtenerTodasLasFunciones();
        Funcion ObtenerPorID(int id);
        Funcion AgregarFuncion(FuncionDTO funcion);
        Funcion ActualizarFuncion(FuncionDTO funcion, int id);
        bool EliminarFuncion(int id);
        string CancelarFuncion(int idFuncion);
        
    }
}