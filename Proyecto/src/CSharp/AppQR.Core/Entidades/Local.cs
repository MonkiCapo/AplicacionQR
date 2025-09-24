using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppQR.Core.Entidades
{
    public class Local
    {
        public int IdLocal { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public List<Sector> Sectores { get; set; } = new List<Sector>();

        public Local()
        { }

        public void AgregarSector(Sector sector)
        {
            Sectores.Add(sector);
        }

        public MostrarSectores()
        {
            foreach (var sector in Sectores)
            {
                Console.WriteLine($"Sector ID: {sector.IdSector}, Nombre: {sector.Nombre}, Capacidad: {sector.Capacidad}");
            }
        }
    }
}