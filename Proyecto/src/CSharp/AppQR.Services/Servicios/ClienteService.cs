using AppQR.Core.Entidades;
using AppQR.Core.Servicios.IServicios;
using FluentValidation;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Servicios.Validadores;

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

        public Cliente AgregarCliente(Cliente clienteDto)
        {
            if (_ClienteRepo.ExisteDNIdeCliente(clienteDto.DNI))
                throw new InvalidOperationException($"Ya existe un cliente con el DNI: {clienteDto.DNI}");

            var clienteNuevo = new Cliente
            {
                DNI = clienteDto.DNI,
                Nombre = clienteDto.Nombre,
                Telefono = clienteDto.Telefono ?? ""
            };

            var resultado = _ClienteValidador.Validate(clienteNuevo);
            if (!resultado.IsValid)
            {
                var errores = string.Join(" | ", resultado.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Error de validación: {errores}");
            }

            return _ClienteRepo.AgregarCliente(clienteNuevo);
        }

        public bool ActualizarCliente(Cliente cliente)
        {
            var resultado = _ClienteValidador.Validate(cliente);
            if (!resultado.IsValid)
            {
                var errores = string.Join(" | ", resultado.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Error de validación: {errores}");
            }

            if (!_ClienteRepo.ExisteDNIdeCliente(cliente.DNI))
                throw new KeyNotFoundException($"No existe un cliente con el DNI {cliente.DNI}.");

            return _ClienteRepo.ActualizarCliente(cliente);
        }

        public bool EliminarCliente(int dni)
        {
            if (!_ClienteRepo.ExisteDNIdeCliente(dni))
                throw new KeyNotFoundException($"No existe un cliente con el DNI: {dni}");

            return _ClienteRepo.EliminarCliente(dni);
        }

        public bool ExisteDNIdeCliente(int dniExistente) => _ClienteRepo.ExisteDNIdeCliente(dniExistente);

    }
}