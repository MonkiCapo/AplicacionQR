using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Servicios.Enums;

namespace AppQR.Core.Entidades
{
    public class Entrada
    {
        public int IdEntrada { get; set; }
        public Tarifa tarifa { get; set; }
        public Orden orden { get; set; }
        public string CodigoQR { get; set; }
        public EEstados Estado { get; set; }

        public Entrada()
        {}
    }
}