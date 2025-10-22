using AppQR.Core.Entidades;
using AppQR.Core.Servicios.IServicios;
using FluentValidation;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Servicios.Validadores;
using AppQR.Core.Dto;

namespace AppQR.Services.Servicios
{
    public class EventoService : IEventoService
    {
        readonly IEventosRepositorio _eventoRepo;
        readonly EventoFluent _eventoValidador;

        public EventoService(IEventosRepositorio eventoRepo, EventoFluent eventoValidador)
        {
            _eventoRepo = eventoRepo;
            _eventoValidador = eventoValidador;
        }

        public IEnumerable<Evento> ObtenerEventos() => _eventoRepo.ObtenerEventos();

        public Evento ObtenerEventoPorID(int id) => _eventoRepo.ObtenerEventoPorID(id);

        public Evento AgregarEvento(EventoDTO dto)
        {
            Evento eventoNuevo = ConvertirDtoAClase(dto);

            var resultado = _eventoValidador.Validate(eventoNuevo);
            if (!resultado.IsValid)
            {
                var errores = string.Join(" | ", resultado.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Error de validación: {errores}");
            }

            return _eventoRepo.AgregarEvento(eventoNuevo);

        }

        public bool ActualizarEvento(EventoDTO dto, int id)
        {
            var eventoActualizado = ConvertirDtoAClase(dto);

            var resultado = _eventoValidador.Validate(eventoActualizado);

            if (!resultado.IsValid)
            {
                var errores = string.Join(" | ", resultado.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Error de validación: {errores}");
            }

            return _eventoRepo.ActualizarEvento(eventoActualizado, id);
        }

        public bool EliminarEvento(int id) => _eventoRepo.EliminarEvento(id);

        public string CancelarEvento(int id) => _eventoRepo.CancelarEvento(id);

        public string PublicarEvento(int id) => _eventoRepo.PublicarEvento(id);

        Evento ConvertirDtoAClase(EventoDTO dto)
        {
            return new Evento
            {
                Nombre = dto.Nombre,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin
            };
        }
    }
}