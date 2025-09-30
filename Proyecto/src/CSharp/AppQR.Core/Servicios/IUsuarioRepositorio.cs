using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Entidades;

namespace AppQR.Core.Servicios
{
    public interface IUsuarioRepositorio
    {
        IEnumerable<Usuario> ObtenerTodosLosUsuarios();
        Usuario ObtenerUsuarioPorID(int id);
        Usuario ObtenerUsuarioPorEmail(string email);
        Usuario AgregarUsuario(Usuario usuario);
        bool ActualizarUsuario(Usuario usuario);
        bool EliminarUsuario(int id);

        Usuario? Login(string loginMail, string loginContraseña);

        bool ExisteUsuario(string emailExistente);
    }
}