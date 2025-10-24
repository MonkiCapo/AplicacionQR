using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Servicios.Repositorios;

namespace AppQR.Services.Servicios
{
    public class AuthService
    {
        readonly IUsuarioRepositorio _usuarioRepo;
        readonly IRefreshTokenRepositorio _refreshTokenRepo;
        readonly RefreshTokenService _refreshTokenService;

        public AuthService(IUsuarioRepositorio usuarioRepo, IRefreshTokenRepositorio refreshTokenRepo, RefreshTokenService refreshTokenService)
        {
            _usuarioRepo = usuarioRepo;
            _refreshTokenRepo = refreshTokenRepo;
            _refreshTokenService = refreshTokenService;
        }
    }
}