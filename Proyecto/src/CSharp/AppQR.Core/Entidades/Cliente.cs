using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string contraseña { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string dni { get; set; }
    }
}