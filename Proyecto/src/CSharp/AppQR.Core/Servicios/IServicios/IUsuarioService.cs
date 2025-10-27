using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using AppQR.Core.Entidades;

namespace AppQR.Core.Servicios.IServicios
{
    public interface IUsuarioService
    {
        IEnumerable<Usuario> ObtenerTodosLosUsuarios();
        Usuario ObtenerUsuarioPorID(int id);
        Usuario ObtenerUsuarioPorEmail(string email);
        Usuario AgregarUsuario(Usuario usuario);
        bool ActualizarUsuario(Usuario usuario, int id);
        bool EliminarUsuario(int id);
        Usuario? Login(string loginMail, string loginContraseña);
        bool ExisteUsuario(string emailExistente);

    }
}