using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using AppQR.Core;
using AppQR.Core.Servicios;
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
            var sql = @"INSERT INTO RefreshToken (Token, Email, Expiration)
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
            var sql = "SELECT * FROM RefreshToken WHERE Token = @Token";
            return Conexion.QueryFirstOrDefault<RefreshToken>(sql, new { Token = token });
        }

        public void EliminarToken(string token)
        {
            var sql = "DELETE FROM RefreshToken WHERE Token = @Token";
            Conexion.Execute(sql, new { Token = token });
        }

        public void EliminarTokensPorEmail(string email)
        {
            var sql = "DELETE FROM RefreshToken WHERE Email = @Email";
            Conexion.Execute(sql, new { Email = email });
        }

        public void ReemplazarToken(int IdUsuario, string nuevoHash, DateTime expiracion)
        {
            var deleteSql = "DELETE FROM RefreshToken WHERE IdUsuario = @idusuario";
            Conexion.Execute(deleteSql, new { idusuario = IdUsuario });

            string TraerUsuario = "SELECT FROM Usuario WHERE IdUsuario = @idusuario";
            var usuario = Conexion.QueryFirstOrDefault<Usuario>(TraerUsuario, new { idusuario = IdUsuario });

            string AgregarSql = @"INSERT INTO RefreshToken (IdUsuario, Token, Email,  Expiration)
                                  VALUES (@idusuario, @token, @email, @expiration);";
            Conexion.Execute(AgregarSql, new { idusuario = IdUsuario, token = nuevoHash, email = usuario?.Email, expiration = expiracion });
        }
    }
}