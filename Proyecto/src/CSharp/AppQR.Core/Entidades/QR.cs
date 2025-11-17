using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Servicios.Enums;

namespace AppQR.Core.Entidades
{
    public class QR
    {
        public int IdQR { get; set; }
        public int IdEntrada { get; set; }
        public string url { get; set; }
        public string Token { get; set; }
        public QR()
        {
            
        }
    }
}