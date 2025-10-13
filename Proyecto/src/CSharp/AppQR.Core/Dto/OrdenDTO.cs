using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core.Dto
{
    public class OrdenDTO
    {
        public string Email { get; set; }
        public string Estado { get; set; }
        public decimal PrecioTotal { get; set; }
        public DateTime Fecha { get; set; }
    }
}