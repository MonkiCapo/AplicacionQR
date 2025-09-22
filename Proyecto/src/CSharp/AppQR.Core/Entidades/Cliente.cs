using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core.Entidades
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Contraseña { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string dni { get; set; }

        public Cliente()
        {}
    }
}