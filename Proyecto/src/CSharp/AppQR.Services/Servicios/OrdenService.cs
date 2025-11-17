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
        readonly IQrRepositorio _qrRepo;
        readonly IQrService _qrService;
        readonly OrdenFluent _OrdenValidador;

        public OrdenService(IOrdenRepositorio ordenRepo, OrdenFluent ordenValidador, IUsuarioRepositorio usuarioRepo, ITarifaRepositorio tarifaRepo, IEntradaRepositorio entradaRepo, IQrRepositorio qrRepo, IQrService qrService)
        {
            _OrdenRepo = ordenRepo;
            _OrdenValidador = ordenValidador;
            _UsuarioRepo = usuarioRepo;
            _TarifaRepo = tarifaRepo;
            _EntradaRepo = entradaRepo;
            _qrRepo = qrRepo;
            _qrService = qrService;
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


        public string PagarOrden(int id, EntradaDTO entradaDto)
        {
            var orden = _OrdenRepo.ObtenerOrdenPorID(id);
            if (orden == null)
                throw new ValidationException($"La orden con el id {id} no existe");

            if (orden.Estado == EEstados.Pagado)
                throw new ValidationException("La orden ya está pagada");

            if (orden.Estado == EEstados.Cancelado || orden.Estado == EEstados.Anulada)
                throw new ValidationException("No se puede pagar una orden cancelada o anulada");

            var tarifa = _TarifaRepo.ObtenerTarifaPorID(entradaDto.IdTarifa);

            if (tarifa == null)
                throw new ValidationException($"La tarifa con ID {entradaDto.IdTarifa} no existe");

            if (tarifa.Stock <= 0)
                throw new ValidationException("No hay stock disponible para la tarifa seleccionada");

            var entradaEmitida = new Entrada
            {
                orden = orden,
                tarifa = tarifa,
                Estado = EEstados.Pagado
            };

            var entradaAlta = _EntradaRepo.AgregarEntrada(entradaEmitida);

            var token = Guid.NewGuid().ToString();

            var url = _qrService.GenerarUrldeQR(token);

            var qr = new QR
            {
                IdEntrada = entradaAlta.IdEntrada,
                url = url,
                Token = token
            };
            
            _qrRepo.AltaQR(qr);

            var resultado = _OrdenRepo.PagarOrden(id);
            return resultado;
        }
    }
}