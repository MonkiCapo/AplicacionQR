using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Dto;
using AppQR.Core.Entidades;
using FluentValidation;

namespace AppQR.Core.Servicios.Validadores
{
    public class FuncionFluent : AbstractValidator<FuncionDTO>
    {
        public FuncionFluent()
        {
            RuleFor(f => f.IdEvento)
                .NotNull().WithMessage("El evento asociado es obligatorio");
            RuleFor(f => f.FechaHora)
                .GreaterThan(DateTime.Now).WithMessage("La fecha debe ser mayor a la actual");
            RuleFor(f => f.Nombre)
                .NotEmpty().WithMessage("El nombre de la funcion no puede estar vacio");
            RuleFor(f => f.Estado)
                .NotNull().WithMessage("El estado es obligatorio");
            }
    }
}