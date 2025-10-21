using AppQR.Core.Entidades;
using AppQR.Core.Servicios.IServicios;
using FluentValidation;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Servicios.Validadores;
using AppQR.Core.Dto;

namespace AppQR.Services.Servicios
{
    public class ClienteService : IClienteService
    {
        readonly IClienteRepositorio _ClienteRepo;
        readonly ClienteFluent _ClienteValidador;
        public ClienteService(ClienteFluent clienteValidador, IClienteRepositorio clienteRepo)
        {
            _ClienteRepo = clienteRepo;
            _ClienteValidador = clienteValidador;
        }

        public IEnumerable<Cliente> ObtenerClientes() => _ClienteRepo.ObtenerClientes();

        public Cliente? ObtenerClientePorDNI(int dni) => _ClienteRepo.ObtenerClientePorDNI(dni);

        public Cliente AgregarCliente(ClienteDTO clienteDto)
        {
            if (_ClienteRepo.ExisteDNIdeCliente(clienteDto.DNI))
                throw new InvalidOperationException($"Ya existe un cliente con el DNI: {clienteDto.DNI}");

            var clienteNuevo = ConvertirDtoClase(clienteDto);

            var resultado = _ClienteValidador.Validate(clienteNuevo);
            if (!resultado.IsValid)
            {
                var errores = string.Join(" | ", resultado.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Error de validacion: {errores}");
            }

            return _ClienteRepo.AgregarCliente(clienteNuevo);
        }

        public bool ActualizarCliente(ClienteDTO dto, int dni)
        {
            var clienteActualizado = ConvertirDtoClase(dto);

            var resultado = _ClienteValidador.Validate(clienteActualizado);

            if (!resultado.IsValid)
            {
                var errores = string.Join(" | ", resultado.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Error de validación: {errores}");
            }

            if (!_ClienteRepo.ExisteDNIdeCliente(dto.DNI))
                throw new KeyNotFoundException($"No existe un cliente con el DNI {dto.DNI}.");

            return _ClienteRepo.ActualizarCliente(clienteActualizado, dni);
        }

        public bool EliminarCliente(int dni)
        {
            if (!_ClienteRepo.ExisteDNIdeCliente(dni))
                throw new KeyNotFoundException($"No existe un cliente con el DNI: {dni}");

            return _ClienteRepo.EliminarCliente(dni);
        }

        public bool ExisteDNIdeCliente(int dniExistente) => _ClienteRepo.ExisteDNIdeCliente(dniExistente);
        
        Cliente ConvertirDtoClase(ClienteDTO clienteDto)
    {
        return new Cliente
        {
            DNI = clienteDto.DNI,
            Nombre = clienteDto.Nombre,
            Telefono = clienteDto.Telefono
        };
    }

    }
}