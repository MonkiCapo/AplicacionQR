using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AppQR.Core;
using AppQR.Core.Servicios;
using AppQR.Core.Entidades;
using AppQR.Core.Dto;
using AppQR.Core.Servicios.Enums;

namespace AppQR.WebAPI.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepo;
        private readonly IClienteRepositorio _clienteRepo;
        private readonly IConfiguration _config;
        private readonly IRefreshTokenRepositorio _refreshTokenRepo;
    }
}