using AppQR.Core.Entidades;
using AppQR.Core.Servicios.IServicios;
using FluentValidation;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Servicios.Validadores;
using AppQR.Core.Dto;

namespace AppQR.Services.Servicios
{
    public class LocalService : ILocalService
    {
        readonly ILocalRepositorio _LocalRepo;
        readonly LocalFluent _LocalValidador;
        public LocalService(ILocalRepositorio localRepo, LocalFluent localValidador)
        {
            _LocalRepo = localRepo;
            _LocalValidador = localValidador;
        }
        
        public IEnumerable<Local> ObtenerLocales () => _LocalRepo.ObtenerLocales();

        public Local? ObtenerLocalPorID(int id) => _LocalRepo.ObtenerLocalPorID(id);

        public Evento AgregarLocal (LocalDTO localDTO)
        {
            if(_LocalRepo.ObtenerLocalPorID(local.IdLocal) != null)
                throw new InvalidOperationException($"Ya existe un local con el ID: {local.IdLocal}");

            var localNuevo = new Local 
            {
                Nombre = LocalDTO.Nombre,
                Direccion =LocalDTO.Direccion
            };

            var resultado =_LocalValidador.Validate(localNuevo);
            if (!resultado.IsValid)
            {
                var errores = string.Join(" |", resultado.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Error de validacion: {errores}");
            }

            return _LocalRepo.AgregarLocal(localNuevo);
        }

        public bool ActualizarLocal (LocalDTO localDTO)
        {
            var localActualizado = new Local
            {
                Nombre = LocalDTO.Nombre,
                Direccion = LocalDTO.Direccion
            };

            var resultado = _LocalValidador.Validate(localActualizado);
            if(!resultado.IsValid)
            {
                var errores = string.Join(" | ", resultado.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Error de validacion: {errores}");
            }

            if(_LocalRepo.ObtenerLocalPorID(local.IdLocal) == null)
                throw new KeyNotFoundException($"No existe un local con ese ID {local.IdLocal}");

            return _LocalRepo.ActualizarLocal(localActualizado);
        }

        public bool EliminarLocal (int id)
        {
            if(_LocalRepo.ObtenerLocalPorID(id) == null)
                throw new KeyNotFoundException($"No existe un local con ese ID: {id}");

            return _LocalRepo.EliminarLocal(id);
        }


        public IEnumerable<Sector> ObtenerSectoresPorLocal(int idLocal) => _LocalRepo.ObtenerSectoresPorLocal(idLocal);

        public Sector? ObtenerSectorPorID(int id) => _LocalRepo.ObtenerSectorPorID(id);

        public Sector AgregarSector(Sector sector, int id)
        {
            if(_LocalRepo.ObtenerSectorPorID(sector.IdSector) != null)
                throw new InvalidOperationException($"Ya existe un sector con ese ID: {sector.IdSector}");

            var sectorNuevo = new Sector
            {
                Capacidad
            }
        }



    }
}