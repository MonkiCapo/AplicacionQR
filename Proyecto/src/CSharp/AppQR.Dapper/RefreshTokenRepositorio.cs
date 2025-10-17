using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using AppQR.Core;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Entidades;
using AppQR.Core.Dto;
using AppQR.Core.Servicios.Enums;

namespace AppQR.Dapper
{
    public class RefreshTokenRepositorio : DapperRepo, IRefreshTokenRepositorio
    {
        public RefreshTokenRepositorio(IDbConnection conexion) : base(conexion) { }

        public int InsertarToken(RefreshToken token)
        {
            var sql = @"INSERT INTO RefreshTokens (Token, Email, Expiration)
                VALUES (@token, @email, @expiration);
                SELECT LAST_INSERT_ID();";

            var id = Conexion.QuerySingle<int>(sql, new
            {
                token = token.Token,
                email = token.Email,
                expiration = token.Expiration
            });

            return id;
        }

        public RefreshToken? ObtenerToken(string token)
        {
            var sql = "SELECT * FROM RefreshTokens WHERE Token = @Token";
            return Conexion.QueryFirstOrDefault<RefreshToken>(sql, new { Token = token });
        }

        public void EliminarToken(string token)
        {
            var sql = "DELETE FROM RefreshTokens WHERE Token = @Token";
            Conexion.Execute(sql, new { Token = token });
        }

        public void EliminarTokensPorEmail(string email)
        {
            var sql = "DELETE FROM RefreshTokens WHERE Email = @Email";
            Conexion.Execute(sql, new { Email = email });
        }

        public void ReemplazarToken(int IdUsuario, string nuevoHash, DateTime expiracion)
        {
            string TraerUsuario = "SELECT * FROM Usuario WHERE IdUsuario = @idusuario";
            var usuario = Conexion.QueryFirstOrDefault<Usuario>(TraerUsuario, new { idusuario = IdUsuario });

            var deleteSql = "DELETE FROM RefreshTokens WHERE Email = @email";
            Conexion.Execute(deleteSql, new { email = usuario.Email });

            

            string InsertSql = @"INSERT INTO RefreshTokens (Token, Email, Expiration)
                                  VALUES (@token, @email, @expiration);";
            Conexion.Execute(InsertSql, new
            {
                token = nuevoHash,
                email = usuario?.Email,
                expiration = expiracion
            });
        }
    }
}