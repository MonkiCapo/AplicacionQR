using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Entidades;
using AppQR.Core.Dto;

namespace AppQR.Core.Servicios.IServicios
{
    public interface IUsuarioService
    {
        Usuario AgregarUsuario(RegisterRequestDTO request);
        Usuario ObtenerUsuarioPorEmail(string email);
        Usuario ObtenerUsuarioPorID(int id);
    }
}