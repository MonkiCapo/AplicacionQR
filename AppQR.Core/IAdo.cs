using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core
{
    public interface IAdo
    {
        void AltaCliente(Cliente cliente);
        void AltaEntrada(Entrada entrada);
        void AltaFuncion(Funcion funcion);
        void AltaLocal(Local local);
        void AltaOrden(Orden orden);
        void AltaSector(Sector sector);
        void AltaTarifa(Tarifa tarifa);
        void AltaEvento(Evento evento);
    }
}