using AppQR.Core.Entidades;
using AppQR.Core.Servicios.IServicios;
using FluentValidation;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Servicios.Validadores;
using AppQR.Core.Dto;

namespace AppQR.Services.Servicios
{
    public class EntradaService : IEntradaService
    {
        readonly IEntradaRepositorio _EntradaRepo;

        public EntradaService(IEntradaRepositorio entradaRepo)
        {
            _EntradaRepo = entradaRepo;
        }

        public IEnumerable<Entrada> ObtenerEntradas() => _EntradaRepo.ObtenerEntradas();
        public Entrada ObtenerEntradaPorID(int id) => _EntradaRepo.ObtenerEntradaPorID(id);
        public string AnularEntrada(int id) => _EntradaRepo.AnularEntrada(id);
    }
}