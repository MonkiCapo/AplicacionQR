using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios;

namespace AppQR.WebAPI.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly AuthService _authService;

        public AuthController(IUsuarioRepositorio usuarioRepo, AuthService authService)
    {
        _usuarioRepo = usuarioRepo;
        _authService = authService;
    }
    }
}