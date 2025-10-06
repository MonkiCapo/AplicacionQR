using AppQR.Core.Servicios.Enums;

namespace AppQR.Core.Entidades;

public class Funcion
{
    public int IdFuncion { get; set; }
    public DateTime FechaHora { get; set; }
    public Evento evento { get; set; }
    public EEstados Estado { get; set; }
    public Funcion()
    {}
}