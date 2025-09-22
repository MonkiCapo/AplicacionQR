using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core
{
    public class Entrada
    {
        public int IdEntrada { get; set; }
        public Tarifa tarifa { get; set; }
        public Orden orden { get; set; }
        public string CodigoQR { get; set; }
        public string Estado { get; set; }

        public Entrada()
        {}
    }
}