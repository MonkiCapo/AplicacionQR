namespace AppQR.Core.Entidades;

public class Funcion
{
    public int IdFuncion { get; set; }
    public DateTime FechaHora { get; set; }
    public Evento evento { get; set; }
    public List<Tarifa> Tarifas { get; set; }

    public Funcion()
    {}
}