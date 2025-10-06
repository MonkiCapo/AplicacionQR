using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Dto;
using AppQR.Core.Entidades;
using FluentValidation;

namespace AppQR.Core.Servicios.Validadores
{
    public class EventoFluent : AbstractValidator<EventoDTO>
    {
        public EventoFluent()
        {
            RuleFor(e => e.Nombre)
                .NotEmpty().WithMessage("El nombre del evento es obligatorio");
            
            RuleFor(e => e.FechaInicio)
                .LessThan(e => e.FechaFin).WithMessage("La fecha de inicio debe ser anterior a la fecha de fin");
            RuleFor(e => e.FechaFin)
                .GreaterThan(e => e.FechaInicio).WithMessage("La fecha de fin tiene que ser superior a la fecha de inicio")
                .GreaterThan(e => DateTime.Now).WithMessage("La fecha final debe ser supeior a la actual");
        }
    }
}