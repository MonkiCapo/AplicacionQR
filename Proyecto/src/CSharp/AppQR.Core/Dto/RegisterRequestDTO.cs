using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core.Dto
{
    public class RegisterRequestDTO
    {
        public string Email { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public ClienteDTO cliente { get; set; }
        public string Rol { get; set; }
    }
}