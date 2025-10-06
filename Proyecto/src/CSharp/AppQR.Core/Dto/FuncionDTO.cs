using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core.Dto
{
    public class FuncionDTO
    {
        public int idEvento { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaHora { get; set; }
        public string Estado { get; set; }
    }
}