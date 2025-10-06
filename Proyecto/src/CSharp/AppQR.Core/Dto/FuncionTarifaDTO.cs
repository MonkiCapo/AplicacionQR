using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Entidades;

namespace AppQR.Core.Dto
{
    public class FuncionTarifaDTO
    {
         public Funcion funcion { get; set; }
         public Tarifa tarifa { get; set; }
    }
}