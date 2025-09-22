using Dapper;
using AppQR.Core;
using AppQR.Core.Servicios;
using AppQR.Core.Entidades;
using System.Data;

namespace AppQR.Dapper;

public class FuncionRepositorio : DapperRepo, IFuncionRepositorio
{
    public FuncionRepositorio(IDbConnection conexion) : base(conexion ){ }

    public Funcion AgregarFuncion(Funcion funcion)
    {
        var sql = @"INSERT INTO Funcion (FechaHora, IdEvento, IdSector)
                VALUES (@FechaHora, @IdEvento, @IdSector); 
                SELECT LAST_INSERT_ID();";
        Conexion.

    }

    public bool ActualizarFuncion(Funcion funcion)
    {
        var sql = @"UPDATE Funcion SET FechaHora = @FechaHora, IdEvento = @IdEvento, IdSector = @IdSector
            WHERE IdFuncion = @IdFuncion";
        using var db = _ado.GetDbConnection();
        var rowsAffected = db.Execute(sql, funcion);
        return rowsAffected > 0;
    }

    public bool EliminarFuncion(int id)
    {
        var sql = @"DELETE FROM Funcion WHERE IdFuncion = @Id";
        using var db = _ado.GetDbConnection();
        var rowsAffected = db.Execute(sql, new { Id = id });
        return rowsAffected > 0;
    }

    public IEnumerable<Funcion> ObtenerTodasLasFunciones()
    {
        var sql = "SELECT * FROM Funcion";
        using var db = _ado.GetDbConnection();
        return db.Query<Funcion>(sql);
    }

    public Funcion ObtenerPorID(int id)
    {
        var sql = "SELECT * FROM Funcion WHERE IdFuncion = @Id";
        using var db = _ado.GetDbConnection();
        var funcion = db.QueryFirstOrDefault<Funcion>(sql, new { Id = id });
        if (funcion == null)
            throw new InvalidOperationException($"No se encontró una función con el ID {id}.");
        return funcion;
    }
}