using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AppQR.Core.Dto;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios.Enums;
using FluentValidation;

namespace AppQR.Core.Servicios.Validadores
{
    public class OrdenFluent : AbstractValidator<OrdenDTO>
    {
        public OrdenFluent()
        {
            RuleFor(x => x.Email)
                .NotNull().WithMessage("El usuario si o si debe estar para realizar la orden")
                .EmailAddress().WithMessage("El formato del email es invalido o el correo es incorrecto");

            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("Se debe colocar estado")
                .Must(estado => estado.Trim().Equals(EEstados.Creado.ToString(), StringComparison.OrdinalIgnoreCase)).WithMessage($"El estado debe ser '{EEstados.Creado}'");

            RuleFor(x => x.PrecioTotal)
                .GreaterThan(0).WithMessage("El total de la orden debe ser mayor a cero");

            RuleFor(x => x.Fecha)
                .NotEmpty().WithMessage("La fecha de la orden es obligatoria")
                .Must(fecha => fecha > DateTime.MinValue).WithMessage("La fecha de la orden no es válida");
        }
    }
}