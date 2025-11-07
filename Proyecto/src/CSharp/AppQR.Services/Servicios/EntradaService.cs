using AppQR.Core.Entidades;
using AppQR.Core.Servicios.IServicios;
using FluentValidation;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Servicios.Validadores;
using AppQR.Core.Dto;
using MySqlX.XDevAPI.Common;
using AppQR.Core.Servicios.Enums;

namespace AppQR.Services.Servicios
{
    public class EntradaService : IEntradaService
    {
        readonly IEntradaRepositorio _EntradaRepo;
        readonly IQrRepositorio _QrRepo;
        readonly IQrService _QrService;

        public EntradaService(IEntradaRepositorio entradaRepo, IQrRepositorio qrRepo, IQrService qrService)
        {
            _EntradaRepo = entradaRepo;
            _QrRepo = qrRepo;
            _QrService = qrService;
        }

        public IEnumerable<Entrada> ObtenerEntradas() => _EntradaRepo.ObtenerEntradas();
        public Entrada ObtenerEntradaPorID(int id) => _EntradaRepo.ObtenerEntradaPorID(id);
        public string AnularEntrada(int id) => _EntradaRepo.AnularEntrada(id);

        public byte[]? ObtenerQR(int id)
        {
            var qr = _QrRepo.ObtenerQr(id);
            if (qr is null)
                return null;

            return _QrService.CrearQR(qr.url);
        }

        public object ValidarQR(int id)
        {
            var entrada = _EntradaRepo.ObtenerEntradaPorID(id);
            if (entrada == null)
                throw new Exception("La entrada no es valida");
            if (entrada.Estado == EEstados.YaUsada)
                throw new Exception("Esta entrada ya fue usada");
            if (entrada.Estado == EEstados.Anulada)
                throw new Exception("La entrada esta anulada");

            _EntradaRepo.EntradaUsada(entrada.IdEntrada);
            return new
            {
                mensaje = "Entrada validada con exito"
            };
        }
    }
}