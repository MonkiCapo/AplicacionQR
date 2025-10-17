using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using AppQR.Core;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Entidades;
using System.Reflection.Metadata;
using AppQR.Core.Servicios.Enums;
using AppQR.Core.Dto;

namespace AppQR.Dapper;

public class FuncionRepositorio : DapperRepo, IFuncionRepositorio
{
    public FuncionRepositorio(IDbConnection conexion) : base(conexion) { }

    public Funcion AgregarFuncion(Funcion funcion)
    {
        var sql = @"INSERT INTO Funcion (Nombre, FechaHora, Estado, IdEvento) 
                VALUES (@nombre, @fechaHora, @estado, @idEvento);
                SELECT LAST_INSERT_ID();";
        var id = Conexion.ExecuteScalar<int>(sql, new
        {
            nombre = funcion.Nombre,
            fechaHora = funcion.FechaHora,
            estado = funcion.Estado,
            idEvento = funcion.evento.IdEvento
        });
        funcion.IdFuncion = id;
        return funcion;
    }

    public bool ActualizarFuncion(Funcion funcion)
    {
        var sql = @"UPDATE Funcion SET Nombre = @nombre, FechaHora = @fechaHora, Estado = @estado, IdEvento = @idEvento
                    WHERE IdFuncion = @idFuncion";
        var rowsAffected = Conexion.Execute(sql, new
        {
            idFuncion = funcion.IdFuncion,
            nombre = funcion.Nombre,
            fechaHora = funcion.FechaHora,
            estado = funcion.Estado,
            idEvento = funcion.evento.IdEvento
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

    public string CancelarFuncion(int idFuncion)
    {
        var sql = "CALL CancelarFuncion(@idFuncion)";
        var mensaje = Conexion.QueryFirstOrDefault<string>(sql, new { idFuncion });
        return mensaje ?? "No se logro cancelar la funcion";
    }
}