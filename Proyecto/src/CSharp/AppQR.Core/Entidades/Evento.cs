using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core.Entidades
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public Local local { get; set; }

        public Evento()
        {}
    }
}