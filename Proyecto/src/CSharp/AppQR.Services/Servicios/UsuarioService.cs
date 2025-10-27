using AppQR.Core.Entidades;
using AppQR.Core.Servicios.IServicios;
using FluentValidation;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Servicios.Validadores;

namespace AppQR.Services.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        readonly IUsuarioRepositorio _UsuarioRepo;
        readonly UsuarioFluent _UsuarioValidador;
        readonly IClienteRepositorio _ClienteRepo;
        public UsuarioService(UsuarioFluent usuarioValidador, IUsuarioRepositorio usuarioRepo, IClienteRepositorio clienteRepo)
        {
            _UsuarioRepo = usuarioRepo;
            _UsuarioValidador = usuarioValidador;
            _ClienteRepo = clienteRepo;
        }

        public IEnumerable<Usuario> ObtenerTodosLosUsuarios() => _UsuarioRepo.ObtenerTodosLosUsuarios();
        public Usuario ObtenerUsuarioPorID(int id) => _UsuarioRepo.ObtenerUsuarioPorID(id);
        public Usuario ObtenerUsuarioPorEmail(string email) => _UsuarioRepo.ObtenerUsuarioPorEmail(email);
        public Usuario AgregarUsuario(Usuario usuario)
        {
            _UsuarioValidador.ValidateAndThrow(usuario);

            if (_UsuarioRepo.ExisteUsuario(usuario.Email))
                throw new InvalidOperationException($"Ya existe un usuario con ese email {usuario.Email}");

            if (_ClienteRepo.ObtenerClientePorDNI(usuario.cliente.DNI) == null)
                throw new ValidationException($"El cliente con ese DNI {usuario.cliente.DNI} no existe :v");

            var usuarioNuevo = new Usuario
            {
                NombreUsuario = usuario.NombreUsuario,
                Email = usuario.Email,
                Contraseña = usuario.Contraseña,
                Rol = usuario.Rol,
                cliente = _ClienteRepo.ObtenerClientePorDNI(usuario.cliente.DNI)
            };

            return _UsuarioRepo.AgregarUsuario(usuarioNuevo);
        }

        public bool ActualizarUsuario(Usuario usuario, int id)
        {
            _UsuarioValidador.ValidateAndThrow(usuario);

            if (_UsuarioRepo.ObtenerUsuarioPorID(id) == null)
                throw new InvalidOperationException($"No existe un usuario con ese Id {id}");

            if (_ClienteRepo.ObtenerClientePorDNI(usuario.cliente.DNI) == null)
                throw new ValidationException($"El cliente con ese DNI {usuario.cliente.DNI} no existe");

            var usuarioActualizado = new Usuario
            {
                NombreUsuario = usuario.NombreUsuario,
                Email = usuario.Email,
                Contraseña = usuario.Contraseña,
                Rol = usuario.Rol,
                cliente = _ClienteRepo.ObtenerClientePorDNI(usuario.cliente.DNI)
            };

            return _UsuarioRepo.ActualizarUsuario(usuarioActualizado, id);
        }

        public bool EliminarUsuario(int id)
        {
            if (_UsuarioRepo.ObtenerUsuarioPorID(id) == null)
                throw new InvalidOperationException($"No existe un usuario con ese Id {id}");

            return _UsuarioRepo.EliminarUsuario(id);
        }

        public Usuario? Login(string loginMail, string loginContraseña)
        {
            var usuario = _UsuarioRepo.Login(loginMail, loginContraseña);
            return usuario;
        }

        public bool ExisteUsuario(string emailExistente) => _UsuarioRepo.ExisteUsuario(emailExistente);
    }
}