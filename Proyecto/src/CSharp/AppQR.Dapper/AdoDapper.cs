﻿using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using AppQR.Core;

namespace AppQR.Dapper;

public class AdoDapper : IAdo
{
    private readonly IDbConnection _conexion;

    public AdoDapper(IDbConnection conexion) => this._conexion = conexion;

    public AdoDapper(string cadena) => _conexion = new MySqlConnection(cadena);

    
}
