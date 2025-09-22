using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core.Entidades
{
    public class Sector
    {
        public int IdSector { get; set; }
        public string Nombre { get; set; }
        public int Capacidad { get; set; }
        public Local local { get; set; }

        public Sector()
        {}
    }
}