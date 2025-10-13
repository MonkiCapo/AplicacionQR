using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Servicios.Enums;

namespace AppQR.Core.Entidades
{
    public class Orden
    {
        public int IdOrden { get; set; }
        public Usuario? usuario { get; set; }
        public EEstados Estado { get; set; }
        public decimal PrecioTotal { get; set; }
        public DateTime Fecha { get; set; }

        public Orden()
        { }
    }
}