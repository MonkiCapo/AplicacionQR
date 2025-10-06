using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Servicios.Enums;

namespace AppQR.Core.Entidades
{
    public class Tarifa
    {
        public int IdTarifa { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public EEstados Estado { get; set; }
        public Funcion funcion { get; set; }

        public Tarifa()
        {}
    }
}