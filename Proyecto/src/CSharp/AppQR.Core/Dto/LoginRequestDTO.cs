using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core.Dto
{
    public class LoginRequestDTO
    {
        public string Email { get; set; }
        public string Contraseña { get; set; }
    }
}