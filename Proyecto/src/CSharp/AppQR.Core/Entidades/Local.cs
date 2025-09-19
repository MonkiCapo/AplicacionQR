using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core
{
    public class Local
    {
        public int IdLocal { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public List<Sector> Sectores { get; set; } = new List<Sector>();

        public Local()
        {}
    }
}