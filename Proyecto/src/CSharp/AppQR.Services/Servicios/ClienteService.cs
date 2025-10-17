using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios.IServicios;
using FluentValidation;
using AppQR.Core.Dto;
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

            var cliente = new Cliente
            {
                DNI = clienteDto.DNI,
                Nombre = clienteDto.Nombre,
                Telefono = clienteDto.Telefono ?? ""
            };

            return _ClienteRepo.AgregarCliente(cliente);
        }

        public bool ActualizarCliente(Cliente cliente)
        {
            
        }

        public bool ExisteDNIdeCliente(int dniExistente) => _ClienteRepo.ExisteDNIdeCliente(dniExistente);

    }
}