using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core
{
    public class Orden
    {
        public int IdOrden { get; set; }
        public Cliente cliente { get; set; }
        public string Estado { get; set; }
        public decimal PrecioTotal { get; set; }
        public DateTime Fecha { get; set; }
        public List<Entrada> Entradas { get; set; }
    }
}