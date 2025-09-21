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
        Task<Cliente> AgregarCliente(Cliente cliente);
        Task<bool> ActualizarCliente(Cliente cliente);
        Task<bool> EliminarCliente(int id);
    }
}