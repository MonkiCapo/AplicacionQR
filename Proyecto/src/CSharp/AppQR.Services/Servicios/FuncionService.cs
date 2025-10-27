using AppQR.Core.Entidades;
using AppQR.Core.Servicios.IServicios;
using FluentValidation;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Servicios.Validadores;
using AppQR.Core.Dto;
using AppQR.Services.Validadores;
using AppQR.Core.Servicios.Enums;


namespace AppQR.Services.Servicios
{
    public class FuncionService : IFuncionService
    {
        readonly IFuncionRepositorio _FuncionRepo;
        readonly FuncionFluent _FuncionValidador;
        readonly IEventosRepositorio _EventoRepo;

        public FuncionService(IFuncionRepositorio funcionRepo, FuncionFluent funcionValidador, IEventosRepositorio eventoRepo)
        {
            _FuncionRepo = funcionRepo;
            _FuncionValidador = funcionValidador;
            _EventoRepo = eventoRepo;
        }

        public IEnumerable<Funcion> ObtenerTodasLasFunciones() => _FuncionRepo.ObtenerTodasLasFunciones();
        public Funcion ObtenerPorID(int id) => _FuncionRepo.ObtenerPorID(id);
        public Funcion AgregarFuncion(FuncionDTO funcionDTO)
        {
            if (_EventoRepo.ObtenerEventoPorID(funcionDTO.idEvento) == null)
                throw new ValidationException($"El evento con ese Id {funcionDTO.idEvento} no existe");

            var funcionNueva = new Funcion
            {
                Nombre = funcionDTO.Nombre,
                FechaHora = funcionDTO.FechaHora,
                Estado = Enum.TryParse<EEstados>(funcionDTO.Estado, true, out var estado) ? estado : EEstados.Creado,
                evento = _EventoRepo.ObtenerEventoPorID(funcionDTO.idEvento)
            };

            var resultado = _FuncionValidador.Validate(funcionNueva);
            if (!resultado.IsValid)
            {
                var errores = string.Join(" | ", resultado.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Error de validacion: {errores}");
            }
            return _FuncionRepo.AgregarFuncion(funcionNueva);
        }

        public bool ActualizarFuncion(FuncionDTO funcionDTO, int id)
        {
            if (_FuncionRepo.ObtenerPorID(id) == null)
                throw new InvalidOperationException($"No existe una funcion con ese Id {id}");

            if (_EventoRepo.ObtenerEventoPorID(funcionDTO.idEvento) == null)
                throw new ValidationException($"El evento con ese Id {funcionDTO.idEvento} no existe");

            var funcionActualizada = new Funcion
            {
                Nombre = funcionDTO.Nombre,
                FechaHora = funcionDTO.FechaHora,
                Estado = Enum.TryParse<EEstados>(funcionDTO.Estado, true, out var estado) ? estado : EEstados.Creado,
                evento = _EventoRepo.ObtenerEventoPorID(funcionDTO.idEvento)
            };

            var resultado = _FuncionValidador.Validate(funcionActualizada);
            if (!resultado.IsValid)
            {
                var errores = string.Join(" | ", resultado.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Error de validacion: {errores}");
            }

            return _FuncionRepo.ActualizarFuncion(funcionActualizada, id);
        }

        public bool EliminarFuncion(int id)
        {
            if (_FuncionRepo.ObtenerPorID(id) == null)
                throw new KeyNotFoundException($"No existe una funcion con ese Id {id}");

            return _FuncionRepo.EliminarFuncion(id);
        }
        
        public string CancelarFuncion(int idFuncion)
        {
            if (_FuncionRepo.ObtenerPorID(idFuncion) == null)
                throw new KeyNotFoundException($"No existe una funcion con ese Id {idFuncion}");

            return _FuncionRepo.CancelarFuncion(idFuncion);
        }
        
    }
}