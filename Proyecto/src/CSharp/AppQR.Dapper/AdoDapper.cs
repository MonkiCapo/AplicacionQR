﻿using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using AppQR.Core;
using AppQR.Core.Entidades;
using AppQR.Core.Servicios;

namespace AppQR.Dapper;

public class AdoDapper : IAdo
{
    private readonly string _conexion;
    public AdoDapper(string conexion)
    {
        _conexion = conexion;
    }

    public IDbConnection GetDbConnection()
    {
        return new MySqlConnection(_conexion);
    }
}
