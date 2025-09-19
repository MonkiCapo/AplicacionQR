using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core.Servicios
{
    public interface IClienteRepositorio
    {
        Task<IEnumerable<Cliente>> ObtenerClientes();
        Task<Cliente> ObtenerClientePorID(int id);
    }
}