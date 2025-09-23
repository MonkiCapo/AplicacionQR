using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Entidades;

namespace AppQR.Core.Servicios
{
    public interface IClienteRepositorio
    {
        IEnumerable<Cliente> ObtenerClientes();
        Cliente ObtenerClientePorID(int id);
        Cliente AgregarCliente(Cliente cliente);
        bool ActualizarCliente(Cliente cliente);
        bool EliminarCliente(int id);
    }
}