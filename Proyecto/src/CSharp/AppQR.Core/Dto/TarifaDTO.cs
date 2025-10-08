using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core.Dto
{
    public class TarifaDTO
    {
        public int IdFuncion { get; set; }
        public string Tipo { get; set; }
        public int Precio { get; set; }
        public int Stock { get; set; }
    }
}