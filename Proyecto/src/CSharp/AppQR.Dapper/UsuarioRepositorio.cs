using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using AppQR.Core;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Entidades;

namespace AppQR.Dapper
{
    public class UsuarioRepositorio : DapperRepo, IUsuarioRepositorio
    {
        public UsuarioRepositorio(IDbConnection conexion) : base(conexion) { }

        public Usuario AgregarUsuario(Usuario usuario)
        {
            var sql = @"INSERT INTO Usuario (NombreUsuario, Email, Contraseña, Rol, DNI) VALUES (@nombreUsuario, @email, @contraseña, @rol, @Dni); 
                SELECT LAST_INSERT_ID();";
            var id = Conexion.ExecuteScalar<int>(sql, new
            {
                nombreUsuario = usuario.NombreUsuario,
                email = usuario.Email,
                contraseña = usuario.Contraseña,
                rol = usuario.Rol.ToString(),
                Dni = usuario.cliente.DNI
            });
            usuario.IdUsuario = id;
            return usuario;
        }

        public bool ActualizarUsuario(Usuario usuario, int id)
        {
            var sql = @"UPDATE Usuario SET NombreUsuario = @nombreUsuario, Email = @email, Contraseña = @contraseña, Rol = @rol, DNI = @Dni
            WHERE IdUsuario = @idUsuario;";
            var rowsAffected = Conexion.Execute(sql, new
            {
                nombreUsuario = usuario.NombreUsuario,
                email = usuario.Email,
                contraseña = usuario.Contraseña,
                rol = usuario.Rol.ToString(),
                Dni = usuario.cliente.DNI
            });
            return rowsAffected > 0;
        }

        public bool EliminarUsuario(int idUsuario)
        {
            var sql = "DELETE FROM Usuario WHERE IdUsuario = @IDUsuario;";
            var rowsAffected = Conexion.Execute(sql, new { IDUsuario = idUsuario });
            return rowsAffected > 0;
        }

        public IEnumerable<Usuario> ObtenerTodosLosUsuarios()
        {
            var sql = @"SELECT u.IdUsuario, u.NombreUsuario, u.Email, u.Contraseña, u.Rol,
                               c.DNI
                        FROM Usuario u
                        INNER JOIN Cliente c ON u.DNI = c.DNI;";

            var usuarios = Conexion.Query<Usuario, Cliente, Usuario>(
                sql,
                (usuario, cliente) =>
                {
                    usuario.cliente = cliente;
                    return usuario;
                },
                splitOn: "DNI"
            );

            return usuarios;
        }

        public Usuario ObtenerUsuarioPorID(int id)
        {
            var sql = "SELECT u.IdUsuario, u.NombreUsuario, u.Email, u.Contraseña, u.Rol, c.DNI, c.Nombre, c.Telefono FROM Usuario u INNER JOIN Cliente c ON u.DNI = c.DNI WHERE u.IdUsuario = @IdUsuario;";


            var usuario = Conexion.Query<Usuario, Cliente, Usuario>(
                sql,
                (u, c) =>
                {
                    u.cliente = c;
                    return u;
                },
                new { IdUsuario = id },
                splitOn: "DNI"
            ).FirstOrDefault();

            return usuario;
        }

        public Usuario ObtenerUsuarioPorEmail(string email)
        {
            var sql = @"SELECT u.IdUsuario, u.NombreUsuario, u.Email, u.Contraseña, u.Rol,
                               c.DNI, c.Nombre, c.Telefono
                        FROM Usuario u
                        INNER JOIN Cliente c ON u.DNI = c.DNI
                        WHERE u.Email = @Email;";

            var usuario = Conexion.Query<Usuario, Cliente, Usuario>(
                sql,
                (u, c) =>
                {
                    u.cliente = c;
                    return u;
                },
                new { Email = email },
                splitOn: "DNI"
            ).FirstOrDefault();

            return usuario;
        }

        public Usuario? Login(string loginMail, string loginContraseña)
        {
            var sql = @"SELECT u.IdUsuario, u.NombreUsuario, u.Email, u.Contraseña, u.Rol,
                       c.DNI, c.Nombre, c.Telefono 
                FROM Usuario u 
                JOIN Cliente c ON u.DNI = c.DNI 
                WHERE u.Email = @email AND u.Contraseña = @contraseña;";
    
            var usuario = Conexion.Query<Usuario, Cliente, Usuario>(
                sql,
                (u, c) =>
                {
                    u.cliente = c;
                    return u;
                },
                new { email = loginMail, contraseña = loginContraseña },
                splitOn: "DNI"
            ).FirstOrDefault();

            return usuario;
        }

        public bool ExisteUsuario(string emailExistente)
        {
            var sql = "SELECT COUNT(1) FROM Usuario WHERE Email = @email;";
            var count = Conexion.ExecuteScalar<int>(sql, new { email = emailExistente });

            return count == 1;
        }

        public bool ActualizarRol(int id, string rol)
        {
            var sql = "UPDATE Usuario SET Rol = @rol WHERE IdUsuario = @id;";
            var rowsAffected = Conexion.Execute(sql, new
            {
                rol = rol,
                id = id
            });
            return rowsAffected > 0;
        }
    }
}