using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Dto;
using AppQR.Core.Entidades;
using FluentValidation;

namespace AppQR.Services.Validadores
{
    public class SectorFluent : AbstractValidator<SectorDTO>
    {
        public SectorFluent()
        {
            RuleFor(s => s.Capacidad)
                .GreaterThan(0).WithMessage("La capacidad del sector debe ser mayor a cero");
        }
        
    }
}