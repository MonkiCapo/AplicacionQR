using AppQR.Core.Entidades;

namespace AppQR.Core.Servicios;

public interface IFuncionRepositorio
{
    IEnumerable<Funcion> ObtenerTodasLasFunciones();
    Funcion ObtenerPorID(int id);
    Funcion AgregarFuncion(Funcion funcion);
    bool ActualizarFuncion(Funcion funcion);
    bool EliminarFuncion(int id);
}