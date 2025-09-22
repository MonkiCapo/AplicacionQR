using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using AppQR.Core;
using AppQR.Core.Servicios;
using AppQR.Core.Entidades;

namespace AppQR.Dapper
{
    public class LocalRepositorio : ILocalRepositorio
    {
        private readonly IAdo _ado;
        public LocalRepositorio(IAdo ado) => _ado = ado;

        
    }
}