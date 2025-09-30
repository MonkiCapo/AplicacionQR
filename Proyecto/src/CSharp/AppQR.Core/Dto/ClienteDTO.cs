using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core.Dto
{
    public class ClienteDTO
    {
        public int DNI { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
    }
}