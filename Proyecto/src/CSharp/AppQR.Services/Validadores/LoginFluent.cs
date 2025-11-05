using FluentValidation;
using AppQR.Core.Dto;

namespace AppQR.Core.Servicios.Validadores
{
    public class LoginFluent : AbstractValidator<LoginRequestDTO>
    {
        public LoginFluent()
        {
            RuleFor(l => l.Email)
                .NotEmpty().WithMessage("El email no puede estar vacío.")
                .EmailAddress().WithMessage("El email debe ser una dirección válida.");

            RuleFor(l => l.Contraseña)
                .NotEmpty().WithMessage("La contraseña no puede estar vacía.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");
        }
    }
}