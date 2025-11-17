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

        public object ValidarQR(string token)
        {
            var qr = _QrRepo.ObtenerPorToken(token);
            if (qr == null)
                throw new Exception("QR inválido");

            var entrada = _EntradaRepo.ObtenerEntradaPorID(qr.IdEntrada);
            if (entrada == null)
                throw new Exception("La entrada no existe");

            if (entrada.Estado == EEstados.YaUsada)
                throw new Exception("La entrada ya fue usada");

            if (entrada.Estado == EEstados.Anulada)
                throw new Exception("La entrada está anulada");

            _EntradaRepo.EntradaUsada(entrada.IdEntrada);

            return new
            {
                mensaje = "Entrada validada correctamente",
                idEntrada = entrada.IdEntrada
            };
        }
    }
}