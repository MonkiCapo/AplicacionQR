using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Entidades;
using FluentValidation;

namespace AppQR.Core.Servicios.Validadores
{
    public class LocalFluent : AbstractValidator<Local>
    {
        public LocalFluent()
        {
            RuleFor(l => l.Nombre)
                .NotEmpty().WithMessage("El nombre del local no puede estar vacío.")
                .MaximumLength(200).WithMessage("El nombre del local no puede superar los 200 caracteres.");

            RuleFor(l => l.Direccion)
                .NotEmpty().WithMessage("La dirección del local no puede estar vacía.")
                .MaximumLength(300).WithMessage("La dirección del local no puede superar los 300 caracteres.");
        }
    }
}