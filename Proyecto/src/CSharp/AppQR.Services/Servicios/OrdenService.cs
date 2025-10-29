using AppQR.Core.Entidades;
using AppQR.Core.Servicios.IServicios;
using FluentValidation;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Servicios.Validadores;
using AppQR.Core.Dto;
using AppQR.Core.Servicios.Enums;

namespace AppQR.Services.Servicios
{
    public class OrdenService : IOrdenService
    {
        readonly IOrdenRepositorio _OrdenRepo;
        readonly OrdenFluent _OrdenValidador;

        public OrdenService(IOrdenRepositorio ordenRepo, OrdenFluent ordenValidador)
        {
            _OrdenRepo = ordenRepo;
            _OrdenValidador = ordenValidador;
        }

        public Orden AgregarOrden(OrdenDTO ordenDto)
        {
            throw new NotImplementedException();
        }

        public string CancelarOrden(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Orden> ObtenerOrdenes()
        {
            throw new NotImplementedException();
        }

        public Orden ObtenerOrdenPorID(int id)
        {
            throw new NotImplementedException();
        }

        public string PagarOrden(int id)
        {
            throw new NotImplementedException();
        }
    }
}