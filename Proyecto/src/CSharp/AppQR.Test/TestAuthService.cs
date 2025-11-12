using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Services.Servicios;
using Moq;
using Xunit;
using AppQR.Core.Dto;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Servicios.Validadores;
using FluentValidation;
using AppQR.Core.Servicios.Utilidades;
using AppQR.Core.Servicios.Enums;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace AppQR.Test
{
    public class TestAuthService
    {
        [Fact]
        public void Debe_RegistrarAl_Usuario()
        {
            var MOQ = new Mock<AuthService>();
            var registerUsuario = new RegisterRequestDTO
            {
                Email = "Luna123@gmail.com",
                NombreUsuario = "Gr1ngo_12",
                Contraseña = "contraseñaSegura1474",
                Rol = "Cliente",
                cliente = new ClienteDTO
                {
                    DNI = 8367263,
                    Nombre = "Gingo naranja",
                    Telefono = "1173692736"
                }
            };
            MOQ.Setup(s => s.RegistrarUsuario(registerUsuario)).Returns(new Usuario { Email = registerUsuario.Email, NombreUsuario = registerUsuario.NombreUsuario, Contraseña = registerUsuario.Contraseña, Rol = ERoles.Cliente, cliente = new Cliente { DNI = registerUsuario.cliente.DNI, Nombre = registerUsuario.cliente.Nombre, Telefono = registerUsuario.cliente.Telefono } });

            var resultado = MOQ.Object.RegistrarUsuario(registerUsuario);

            Assert.NotNull(resultado);
        }

        [Fact]
        public void Debe_Crear_LoginDeUsuario()
        {
            var MOQ = new Mock<AuthService>();
            var loginUsuario = new LoginRequestDTO
            {
                Email = "Celeste25@gmail.com",
                Contraseña = "GatitosCute56"
            };
            MOQ.Setup(s => s.LoginUsuario(loginUsuario)).Returns(loginUsuario);

            var resultado = MOQ.Object.LoginUsuario(loginUsuario);

            Assert.NotNull(resultado);
            
        }
    }
}