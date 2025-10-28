using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Dto;
using AppQR.Core.Entidades;

namespace AppQR.Core.Servicios.IServicios
{
    public interface ITarifaService
    {
        IEnumerable<Tarifa> ObtenerTodasLasTarifas();
        Tarifa ObtenerTarifaPorID(int id);
        IEnumerable<Tarifa> ObtenerTarifasPorFuncion(int idFuncion);
        Tarifa AgregarTarifa(TarifaDTO tarifaDto);
        bool ActualizarTarifa(TarifaDTO tarifaDto);
        bool EliminarTarifa(int id);
    }
}