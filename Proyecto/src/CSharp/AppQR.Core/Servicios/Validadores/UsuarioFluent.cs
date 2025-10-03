using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Entidades;
using FluentValidation;

namespace AppQR.Core.Servicios.Validadores
{
    public class UsuarioFluent : AbstractValidator<Usuario>
    {
        public UsuarioFluent()
        {
            RuleFor(u => u.NombreUsuario)
                .NotEmpty().WithMessage("El nombre de usuario no puede estar vacío.")
                .MinimumLength(3).WithMessage("El nombre de usuario debe tener al menos 3 caracteres.")
                .MaximumLength(50).WithMessage("El nombre de usuario no puede superar los 50 caracteres.");

            RuleFor(u => u.Contraseña)
                .NotEmpty().WithMessage("La contraseña no puede estar vacía.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("El email no puede estar vacío.")
                .EmailAddress().WithMessage("El email debe ser una dirección válida.");

            RuleFor(u => u.Rol)
                .NotEmpty().WithMessage("El rol no puede estar vacío.");
                
        }
    }
}