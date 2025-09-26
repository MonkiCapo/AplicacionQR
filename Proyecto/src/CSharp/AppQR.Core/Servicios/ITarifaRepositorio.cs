using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Entidades;

namespace AppQR.Core.Servicios
{
    public interface ITarifaRepositorio
    {
        public IEnumerable<Tarifa> ObtenerTodasLasTarifas();
        Tarifa ObtenerTarifaPorID(int id);
        Tarifa AgregarTarifa(Tarifa tarifa);
        bool ActualizarTarifa(Tarifa tarifa);
        bool EliminarTarifa(int id);
    }
}