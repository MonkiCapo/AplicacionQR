using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Dto;
using AppQR.Core.Entidades;
using FluentValidation;

namespace AppQR.Core.Servicios.Validadores
{
    public class ClienteFluent : AbstractValidator<Cliente>
    {
        public ClienteFluent()
        {
            RuleFor(c => c.DNI)
                .GreaterThan(0).WithMessage("El DNI debe ser un número positivo.");


            RuleFor(c => c.Nombre)
                .NotEmpty().WithMessage("El nombre no puede estar vacío.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(c => c.Telefono)
                .NotEmpty().WithMessage("El teléfono no puede estar vacío.")
                .Matches(@"^\+?\d{7,15}$").WithMessage("El teléfono debe ser un número válido.");
        }
    }
}