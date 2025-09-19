using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core
{
    public interface IAdo
    {
        IDbConnection GetDbConnection();
    }
       



        // void AltaCliente(Cliente cliente);
        // void AltaEntrada(Entrada entrada);
        // void AltaFuncion(Funcion funcion);
        // void AltaLocal(Local local);
        // void AltaOrden(Orden orden);
        // void AltaSector(Sector sector);
        // void AltaTarifa(Tarifa tarifa);
        // void AltaEvento(Evento evento);


        // Cliente ObtenerClientePorId(int id);
        // IEnumerable<Cliente> ObtenerClientes();

        // Entrada ObtenerEntradaPorId(int id);
        // IEnumerable<Entrada> ObtenerEntradasPorCliente(int clienteId);

        // Evento ObtenerEventoPorId(int id);
        // IEnumerable<Evento> ObtenerEventos();

        // Funcion ObtenerFuncionPorId(int id);
        // IEnumerable<Funcion> ObtenerFuncionesPorEvento(int eventoId);


        // void ActualizarCliente(Cliente cliente);
        // void ActualizarEvento(Evento evento);

        //         void EliminarCliente(int id);
        // void EliminarEvento(int id);
    }
}