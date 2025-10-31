using AppQR.Core.Entidades;
using AppQR.Core.Servicios.IServicios;
using FluentValidation;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Servicios.Validadores;
using AppQR.Core.Dto;
using AppQR.Core.Servicios.Enums;

namespace AppQR.Services.Servicios
{
    public class EntradaService : IEntradaService
    {
        readonly IEntradaRepositorio _EntradaRepo;
        readonly EntradasFluent _EntradaValidador;
        readonly ITarifaRepositorio _TarifaRepo;
        readonly IOrdenRepositorio _OrdenRepo;

        public EntradaService(IEntradaRepositorio entradaRepo, EntradasFluent entradaValidador, ITarifaRepositorio tarifaRepo, IOrdenRepositorio ordenRepo)
        {
            _EntradaRepo = entradaRepo;
            _EntradaValidador = entradaValidador;
            _TarifaRepo = tarifaRepo;
            _OrdenRepo = ordenRepo;
        }

        public IEnumerable<Entrada> ObtenerEntradas() => _EntradaRepo.ObtenerEntradas();
        public Entrada ObtenerEntradaPorID(int id) => _EntradaRepo.ObtenerEntradaPorID(id);
        public Entrada AgregarEntrada(EntradaDTO entradaDTO)
        {
            _EntradaValidador.ValidateAndThrow(entradaDTO);

            if (_TarifaRepo.ObtenerTarifaPorID(entradaDTO.IdTarifa) == null)
                throw new ValidationException($"La tarifa con ese Id {entradaDTO.IdTarifa} no existe");

            if (_OrdenRepo.ObtenerOrdenPorID(entradaDTO.IdOrden) == null)
                throw new ValidationException($"La orden con ese Id {entradaDTO.IdOrden} no existe");

            var entradaNueva = new Entrada
            {
                tarifa = _TarifaRepo.ObtenerTarifaPorID(entradaDTO.IdTarifa),
                orden = _OrdenRepo.ObtenerOrdenPorID(entradaDTO.IdOrden),
                Estado = Enum.TryParse<EEstados>(entradaDTO.Estado, true, out var estado) ? estado : EEstados.Creado,
            };
            return _EntradaRepo.AgregarEntrada(entradaNueva);
        }

        public bool ActualizarEntrada(EntradaDTO entradaDTO, int id)
        {
            _EntradaValidador.ValidateAndThrow(entradaDTO);

            if (_EntradaRepo.ObtenerEntradaPorID(id) == null)
                throw new InvalidOperationException($"No existe una entrada con ese Id {id}");

            if (_TarifaRepo.ObtenerTarifaPorID(entradaDTO.IdTarifa) == null)
                throw new ValidationException($"La tarifa con ese Id {entradaDTO.IdTarifa} no existe");

            if (_OrdenRepo.ObtenerOrdenPorID(entradaDTO.IdOrden) == null)
                throw new ValidationException($"La orden con ese Id {entradaDTO.IdOrden} no existe");

            var entradaActualizada = new Entrada
            {
                tarifa = _TarifaRepo.ObtenerTarifaPorID(entradaDTO.IdTarifa),
                orden = _OrdenRepo.ObtenerOrdenPorID(entradaDTO.IdOrden),
                Estado = Enum.TryParse<EEstados>(entradaDTO.Estado, true, out var estado) ? estado : EEstados.Creado,
            };

            return _EntradaRepo.ActualizarEntrada(entradaActualizada, id);
        }

        public bool EliminarEntrada(int id)
        {
            if (_EntradaRepo.ObtenerEntradaPorID(id) == null)
                throw new KeyNotFoundException($"No existe una entrada con ese Id {id}");
            
            return _EntradaRepo.EliminarEntrada(id);
        }

    }
}