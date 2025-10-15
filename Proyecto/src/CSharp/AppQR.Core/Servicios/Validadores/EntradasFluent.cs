using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using AppQR.Core.Entidades;
using AppQR.Core.Dto;

namespace AppQR.Core.Servicios.Validadores
{
    public class EntradasFluent : AbstractValidator<EntradaDTO>
    {
        public EntradasFluent()
        {
            RuleFor(e => e.IdTarifa)
                .NotNull().WithMessage("La entrada debe tener una tarifa");

            RuleFor(e => e.IdOrden)
                .NotNull().WithMessage("La entrada debe tener una orden de compra");

            RuleFor(e => e.Estado)
                .NotNull().WithMessage("La entrada debe tener un estado");
        }
    }
}