using AppQR.Core.Entidades;
using AppQR.Core.Servicios.IServicios;
using FluentValidation;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Servicios.Validadores;
using AppQR.Core.Dto;
using AppQR.Core.Servicios.Enums;
using System.Reflection.Metadata;

namespace AppQR.Services.Servicios
{
    public class OrdenService : IOrdenService
    {
        readonly IOrdenRepositorio _OrdenRepo;
        readonly IUsuarioRepositorio _UsuarioRepo;
        readonly IEntradaRepositorio _EntradaRepo;
        readonly ITarifaRepositorio _TarifaRepo;
        readonly OrdenFluent _OrdenValidador;

        public OrdenService(IOrdenRepositorio ordenRepo, OrdenFluent ordenValidador, IUsuarioRepositorio usuarioRepo)
        {
            _OrdenRepo = ordenRepo;
            _OrdenValidador = ordenValidador;
            _UsuarioRepo = usuarioRepo;
        }

        public Orden AgregarOrden(OrdenDTO ordenDto)
        {
            _OrdenValidador.ValidateAndThrow(ordenDto);

            if (_UsuarioRepo.ObtenerUsuarioPorEmail(ordenDto.Email) == null)
                throw new ValidationException($"El usuario con el email {ordenDto.Email} no existe");

            var ordenHecha = new Orden
            {
                Estado = EEstados.Creado,
                Fecha = ordenDto.Fecha,
                PrecioTotal = ordenDto.PrecioTotal,
                usuario = _UsuarioRepo.ObtenerUsuarioPorEmail(ordenDto.Email)
            };

            return _OrdenRepo.AgregarOrden(ordenHecha);
        }

        public string CancelarOrden(int id) => _OrdenRepo.CancelarOrden(id);

        public IEnumerable<Orden> ObtenerOrdenes() => _OrdenRepo.ObtenerOrdenes();
        public Orden ObtenerOrdenPorID(int id) => _OrdenRepo.ObtenerOrdenPorID(id);


        public string PagarOrden(int id)
        {
            throw new NotImplementedException();
        }
    }
}