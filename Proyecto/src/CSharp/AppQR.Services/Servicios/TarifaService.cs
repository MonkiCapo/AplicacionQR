using AppQR.Core.Entidades;
using AppQR.Core.Servicios.IServicios;
using FluentValidation;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Servicios.Validadores;
using AppQR.Core.Dto;
using AppQR.Core.Servicios.Enums;

namespace AppQR.Services.Servicios
{
    public class TarifaService : ITarifaService
    {
        readonly ITarifaRepositorio _TarifaRepo;
        readonly TarifaFluent _TarifaValidador;
        readonly IFuncionRepositorio _FuncionRepo;

        public TarifaService(ITarifaRepositorio tarifaRepo, TarifaFluent tarifaValidador, IFuncionRepositorio funcionRepo)
        {
            _TarifaRepo = tarifaRepo;
            _TarifaValidador = tarifaValidador;
            _FuncionRepo = funcionRepo;
        }

        public bool ActualizarTarifa(TarifaDTO tarifaDto, int id)
        {
            _TarifaValidador.ValidateAndThrow(tarifaDto);

            if (_TarifaRepo.ObtenerTarifaPorID(id) == null)
                throw new ValidationException($"La tarifa con el ID {id} no existe");

            if (_FuncionRepo.ObtenerPorID(tarifaDto.IdFuncion) == null)
                throw new ValidationException($"La función con el Id {tarifaDto.IdFuncion} no existe");

            var tarifaActualizada = new Tarifa
            {
                Tipo = Enum.TryParse<ETipoTarifa>(tarifaDto.Tipo, true, out var tipo) ? tipo : ETipoTarifa.General,
                Precio = tarifaDto.Precio,
                Stock = tarifaDto.Stock,
                Estado = EEstados.Activo,
                funcion = _FuncionRepo.ObtenerPorID(tarifaDto.IdFuncion)
            };
            return _TarifaRepo.ActualizarTarifa(tarifaActualizada, id);
        }

        public Tarifa AgregarTarifa(TarifaDTO tarifaDto)
        {
            _TarifaValidador.ValidateAndThrow(tarifaDto);
            if (_FuncionRepo.ObtenerPorID(tarifaDto.IdFuncion) == null)
                throw new ValidationException($"La función con el Id {tarifaDto.IdFuncion} no existe");
            var tarifaNueva = new Tarifa
            {
                Tipo = Enum.TryParse<ETipoTarifa>(tarifaDto.Tipo, true, out var tipo) ? tipo : ETipoTarifa.General,
                Precio = tarifaDto.Precio,
                Stock = tarifaDto.Stock,
                Estado = EEstados.Activo,
                funcion = _FuncionRepo.ObtenerPorID(tarifaDto.IdFuncion)
            };
            return _TarifaRepo.AgregarTarifa(tarifaNueva);
        }

        public bool EliminarTarifa(int id) => _TarifaRepo.EliminarTarifa(id);



        public Tarifa ObtenerTarifaPorID(int id) => _TarifaRepo.ObtenerTarifaPorID(id);

        public IEnumerable<Tarifa> ObtenerTarifasPorFuncion(int idFuncion) => _TarifaRepo.ObtenerTarifasPorFuncion(idFuncion);

        public IEnumerable<Tarifa> ObtenerTodasLasTarifas() => _TarifaRepo.ObtenerTodasLasTarifas();
    }
}