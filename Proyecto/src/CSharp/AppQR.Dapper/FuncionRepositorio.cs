using Dapper;
using AppQR.Core;
using AppQR.Core.Servicios;
using AppQR.Core.Entidades;
using System.Data;

namespace AppQR.Dapper;

public class FuncionRepositorio : DapperRepo, IFuncionRepositorio
{
    public FuncionRepositorio(IDbConnection conexion) : base(conexion) { }

    public Funcion AgregarFuncion(Funcion funcion)
    {
        var sql = @"INSERT INTO Funcion (FechaHora, IdEvento) 
                VALUES (@fechaHora, @idEvento);
                SELECT LAST_INSERT_ID();";
        var id = Conexion.ExecuteScalar<int>(sql, new
        {
            fechaHora = funcion.FechaHora,
            idEvento = funcion.evento.IdEvento
        });
        funcion.IdFuncion = id;
        return funcion;
    }

    public bool ActualizarFuncion(Funcion funcion)
    {
        var sql = @"UPDATE Funcion SET FechaHora = @fechaHora, IdEvento = @idEvento
                    WHERE IdFuncion = @idFuncion";
        var rowsAffected = Conexion.Execute(sql, new
        {
            fechaHora = funcion.FechaHora,
            idEvento = funcion.evento.IdEvento,
            idFuncion = funcion.IdFuncion
        });
        return rowsAffected > 0;
    }

    public bool EliminarFuncion(int id)
    {
        var sql = @"DELETE FROM Funcion WHERE IdFuncion = @Id";
        var rowsAffected = Conexion.Execute(sql, new { Id = id });
        return rowsAffected > 0;
    }

    public IEnumerable<Funcion> ObtenerTodasLasFunciones()
    {
        var sql = "SELECT * FROM Funcion";
        return Conexion.Query<Funcion>(sql);
    }

    public Funcion ObtenerPorID(int id)
    {
        var sql = "SELECT * FROM Funcion WHERE IdFuncion = @Id";
        var funcion = Conexion.QueryFirstOrDefault<Funcion>(sql, new { Id = id });
        return funcion;
    }
}