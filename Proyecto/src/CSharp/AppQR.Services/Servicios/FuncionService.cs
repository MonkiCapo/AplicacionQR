using AppQR.Core.Entidades;
using AppQR.Core.Servicios.IServicios;
using FluentValidation;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Servicios.Validadores;
using AppQR.Core.Dto;
using AppQR.Core.Servicios.Enums;

namespace AppQR.Services.Servicios
{
    public class FuncionService : IFuncionService
    {
        readonly IFuncionRepositorio _FuncionRepo;
        readonly IEventosRepositorio _EventoRepo;
        readonly FuncionFluent _FuncionValidador;

        public FuncionService(FuncionFluent funcionValidador, IFuncionRepositorio funcionRepo, IEventosRepositorio eventoRepo)
        {
            _FuncionRepo = funcionRepo;
            _EventoRepo = eventoRepo;
            _FuncionValidador = funcionValidador;
        }

        public IEnumerable<Funcion> ObtenerTodasLasFunciones() => _FuncionRepo.ObtenerTodasLasFunciones();

        public Funcion? ObtenerPorID(int id) => _FuncionRepo.ObtenerPorID(id);

        public Funcion AgregarFuncion(FuncionDTO dto)
        {
            _FuncionValidador.ValidateAndThrow(dto);
            var eventoExistente = _EventoRepo.ObtenerEventoPorID(dto.idEvento);
            if (eventoExistente == null)
            {
                throw new KeyNotFoundException($"El evento con ID: {dto.idEvento} No existe");
            }

            var FuncionNueva = new Funcion
            {
                Nombre = dto.Nombre,
                FechaHora = dto.FechaHora,
                Estado = Enum.Parse<EEstados>(dto.Estado, ignoreCase: true),
                evento = eventoExistente
            };

            return _FuncionRepo.AgregarFuncion(FuncionNueva);
        }

        public bool ActualizarFuncion(FuncionDTO funcion, int id)
        {
            throw new NotImplementedException();
        }

        public bool EliminarFuncion(int id)
        {
            throw new NotImplementedException();
        }

        public string CancelarFuncion(int idFuncion)
        {
            throw new NotImplementedException();
        }
    }
}