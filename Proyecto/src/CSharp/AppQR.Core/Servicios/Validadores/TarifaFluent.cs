using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Dto;
using AppQR.Core.Entidades;
using FluentValidation;


namespace AppQR.Core.Servicios.Validadores
{
    public class TarifaFluent : AbstractValidator<TarifaDTO>
    {
        public TarifaFluent()
        {
            RuleFor(t => t.IdFuncion)
            .NotNull().WithMessage("La función asociada es obligatoria");

            RuleFor(t => t.Tipo)
                .NotEmpty().WithMessage("El tipo de tarifa es obligatorio");

            RuleFor(t => t.Precio)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0");

            RuleFor(t => t.Stock)
                .GreaterThan(0).WithMessage("El stock debe ser mayor a 0");
        }
    }
}