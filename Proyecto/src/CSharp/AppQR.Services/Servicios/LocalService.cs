using AppQR.Core.Entidades;
using AppQR.Core.Servicios.IServicios;
using FluentValidation;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Servicios.Validadores;
using AppQR.Core.Dto;
using AppQR.Services.Validadores;


namespace AppQR.Services.Servicios
{
    public class LocalService : ILocalService
    {
        readonly ILocalRepositorio _LocalRepo;
        readonly LocalFluent _LocalValidador;
        readonly SectorFluent _SectorValidador;

        public LocalService(ILocalRepositorio localRepo, LocalFluent localValidador, SectorFluent sectorValidador)
        {
            _LocalRepo = localRepo;
            _LocalValidador = localValidador;
            _SectorValidador = sectorValidador;
        }
        
        public IEnumerable<Local> ObtenerLocales () => _LocalRepo.ObtenerLocales();

        public Local ObtenerLocalPorID(int id) => _LocalRepo.ObtenerLocalPorID(id);

        public Local AgregarLocal (LocalDTO localDTO)
        {
            _LocalValidador.ValidateAndThrow(localDTO);

            //if(_LocalRepo.ObtenerLocales().Any(l => l.Nombre == localDTO.Nombre && l.Direccion == localDTO.Direccion))
                //throw new InvalidOperationException($"Ya existe un local con ese nombre y dirección");

            var localNuevo = new Local 
            {
                Nombre = localDTO.Nombre,
                Direccion =localDTO.Direccion
            };

            return _LocalRepo.AgregarLocal(localNuevo);
        }

        public bool ActualizarLocal (LocalDTO localDTO , int id)
        {
            _LocalValidador.ValidateAndThrow(localDTO);

            var localActualizado = new Local
            {
                Nombre = localDTO.Nombre,
                Direccion = localDTO.Direccion
            };

            // var resultado = _LocalValidador.Validate(localActualizado);
            // if(!resultado.IsValid)
            // {
            //     var errores = string.Join(" | ", resultado.Errors.Select(e => e.ErrorMessage));
            //     throw new ValidationException($"Error de validacion: {errores}");
            // }

            //if(_LocalRepo.ObtenerLocalPorID(IdLocal) == null)
            // throw new KeyNotFoundException($"No existe un local con ese ID {local.IdLocal}");

            return _LocalRepo.ActualizarLocal(localActualizado, id);
        }

        public bool EliminarLocal (int id)
        {
            if(_LocalRepo.ObtenerLocalPorID(id) == null)
                throw new KeyNotFoundException($"No existe un local con ese Id: {id}");

            return _LocalRepo.EliminarLocal(id);
        }


        public IEnumerable<Sector> ObtenerSectoresPorLocal(int idLocal) => _LocalRepo.ObtenerSectoresPorLocal(idLocal);

        public Sector ObtenerSectorPorID(int id) => _LocalRepo.ObtenerSectorPorID(id);

        public Sector AgregarSector(SectorDTO sectorDTO, int id)
        {
            _SectorValidador.ValidateAndThrow(sectorDTO);

            var local = _LocalRepo.ObtenerLocalPorID(id);
            var sectorNuevo = new Sector
            {
                Capacidad = sectorDTO.Capacidad,
                local = local
            };

            // var resultado = _SectorValidador.Validate(sectorNuevo);
            // if (!resultado.IsValid)
            // {
            //     var errores = string.Join(" | ", resultado.Errors.Select(e => e.ErrorMessage));
            //     throw new ValidationException($"Error de validacion: {errores}");
            // }

            return _LocalRepo.AgregarSector(sectorNuevo, id);
        }

        public bool ActualizarSector(SectorDTO sectorDTO, int id)
        {
            _SectorValidador.ValidateAndThrow(sectorDTO);

            var sectorActualizado = new Sector
            {
                Capacidad = sectorDTO.Capacidad
            };

            // var resultado = _SectorValidador.Validate(sectorActualizado);
            // if (!resultado.IsValid)
            // {
            //     var errores = string.Join(" | ", resultado.Errors.Select(e => e.ErrorMessage));
            //     throw new ValidationException($"Error de validacion: {errores}");
            // }

            return _LocalRepo.ActualizarSector(sectorActualizado, id);
        }

        public bool EliminarSector(int id)
        {
            if (_LocalRepo.ObtenerSectorPorID(id) == null)
                throw new KeyNotFoundException($"No existe un sector con ese Id:{id}");

            return _LocalRepo.EliminarSector(id);
        }


    }
}