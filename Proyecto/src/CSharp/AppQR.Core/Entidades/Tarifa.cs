using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core
{
    public class Tarifa
    {
        public int IdTarifa { get; set; }
        public decimal Precio { get; set; }
        public int stock { get; set; }
        public string Estado { get; set; }
        public Funcion funcion { get; set; }

        public Tarifa()
        {}
    }
}