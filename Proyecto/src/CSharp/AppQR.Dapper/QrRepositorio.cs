using System.Data;
using Dapper;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios.Repositorios;

namespace AppQR.Dapper
{
    public class QrRepositorio : DapperRepo, IQrRepositorio
    {
        public QrRepositorio(IAdo _ado) : base(_ado) { }
        
        public QR? ObtenerQr(int idQR)
        {
            var sql = "SELECT IdQR, IdEntrada, url FROM QR WHERE IdQR = @IdQR;";
            var qr = Conexion.QueryFirstOrDefault<QR>(sql, new { IdQR = idQR });
            return qr;
        }

        public QR AltaQR(QR qr)
        {
            var sql = @"INSERT INTO QR (IdEntrada, url) 
                        VALUES (@idEntrada, @Url);
                        SELECT LAST_INSERT_ID();";

            var idQr = Conexion.ExecuteScalar<int>(sql, new
            {
                idEntrada = qr.IdEntrada,
                Url = qr.url
            });

            qr.IdQR = idQr;
            return qr;
        }
    }
}