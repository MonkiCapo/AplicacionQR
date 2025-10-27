using AppQR.Core.Entidades;

namespace AppQR.Core.Servicios.Repositorios;

public interface IFuncionRepositorio
{
    IEnumerable<Funcion> ObtenerTodasLasFunciones();
    Funcion ObtenerPorID(int id);
    Funcion AgregarFuncion(Funcion funcion);
    bool ActualizarFuncion(Funcion funcion, int id);
    bool EliminarFuncion(int id);
    string CancelarFuncion(int idFuncion);
}