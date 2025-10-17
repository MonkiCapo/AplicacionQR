using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Entidades;

namespace AppQR.Core.Servicios.IServicios
{
    public interface IClienteService
    {
        IEnumerable<Cliente> ObtenerClientes();
        Cliente ObtenerClientePorDNI(int dni);
        Cliente AgregarCliente(Cliente cliente);
        bool ActualizarCliente(Cliente cliente);
        bool EliminarCliente(int dni);
        bool ExisteDNIdeCliente(int dniExistente);
    }
}