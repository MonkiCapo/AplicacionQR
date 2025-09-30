using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Servicios.Enums;

namespace AppQR.Core.Entidades
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Email { get; set; }
        public ERoles Rol { get; set; }
        public Cliente cliente { get; set; }

        public Usuario()
        {}
    }
}