using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core.Dto
{
    public class RefreshTokenDTO
    {
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!; 
    }
}