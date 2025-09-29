using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppQR.Core.Entidades;

namespace AppQR.Core.Servicios
{
    public interface IUsuarioRepositorio
    {
        public IEnumerable<Usuario> ObtenerTodosLosUsuarios();
        public Usuario ObtenerUsuarioPorID(int id);
        public Usuario ObtenerUsuarioPorEmail(string email);
        public Usuario AgregarUsuario(Usuario usuario);
        public bool ActualizarUsuario(Usuario usuario);
        public bool EliminarUsuario(int id);
    }
}