using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core.Entidades
{
    public class RefreshToken
    {
        public int IdToken { get; set; }
        public string Token { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime Expiration { get; set; }

        public RefreshToken()
        {}
    }
}