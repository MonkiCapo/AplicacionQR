namespace AppQR.Core.Entidades;

public class Funcion
{
    public int IdFuncion { get; set; }
    public DateTime FechaHora { get; set; }
    public Evento evento { get; set; }
    public Sector sector { get; set; }

    public Funcion()
    {}
}