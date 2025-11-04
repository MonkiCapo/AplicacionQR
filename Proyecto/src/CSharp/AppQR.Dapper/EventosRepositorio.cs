using System.Data;
using Dapper;
using MySql.Data.MySqlClient;
using AppQR.Core;
using AppQR.Core.Servicios.Repositorios;
using AppQR.Core.Entidades;
using System.Reflection.Metadata;
using AppQR.Core.Servicios.Enums;
using AppQR.Core.Dto;

namespace AppQR.Dapper
{
    public class EventosRepositorio : DapperRepo, IEventosRepositorio
    {
        public EventosRepositorio(IDbConnection conexion) : base(conexion) { }


        public Evento AgregarEvento(Evento evento)
        {
            var sql = @"INSERT INTO Evento (Nombre, Estado, FechaInicio, FechaFin) VALUES (@nombre, @estado, @fechaInicio, @fechaFin); 
                SELECT LAST_INSERT_ID();";
            var id = Conexion.ExecuteScalar<int>(sql, new
            {
                nombre = evento.Nombre,
                estado = evento.Estado.ToString(),
                fechaInicio = evento.FechaInicio,
                fechaFin = evento.FechaFin
            });
            evento.IdEvento = id;
            return evento;
        }

        public bool ActualizarEvento(Evento evento, int id)
        {
            var sql = @"UPDATE Evento 
                        SET Nombre = @nombre, Estado = @estado, FechaInicio = @fechaInicio, FechaFin = @fechaFin
                        WHERE IdEvento = @idEvento";

            var rowsAffected = Conexion.Execute(sql, new
            {
                nombre = evento.Nombre,
                estado = evento.Estado.ToString(),
                fechaInicio = evento.FechaInicio,
                fechaFin = evento.FechaFin,
                idEvento = id
            });
            return rowsAffected > 0;
        }

        public bool EliminarEvento(int id)
        {
            var sql = @"DELETE FROM Evento WHERE IdEvento = @Id";
            var rowsAffected = Conexion.Execute(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public IEnumerable<Evento> ObtenerEventos()
        {
            var sql = "SELECT * FROM Evento";
            return Conexion.Query<Evento>(sql);
        }

        public Evento ObtenerEventoPorID(int id)
        {
            var sql = "SELECT * FROM Evento WHERE IdEvento = @Id";
            var evento = Conexion.QueryFirstOrDefault<Evento>(sql, new { Id = id });
            return evento;
        }

        public string CancelarEvento(int id)
        {
            var evento = ObtenerEventoPorID(id);
            if (evento == null)
                throw new ArgumentNullException("No se pudo encontrar el evento");
            if (evento.Estado == EEstados.Cancelado)
                throw new Exception("Este evento ya fue cancelado");

            try
            {
                var funciones = Conexion.Query<Funcion>(
                    "SELECT * FROM Funcion WHERE IdEvento = @idevento",
                    new { idevento = id }
                );

                foreach (var funcion in funciones)
                {
                    var entradas = Conexion.Query<Entrada>(
                        @"SELECT *
                        FROM Entrada e
                        INNER JOIN Tarifa t ON e.IdTarifa = t.IdTarifa
                        WHERE t.IdFuncion = @idfuncion",
                            new { idfuncion = funcion.IdFuncion }
                    );

                    foreach (var entrada in entradas)
                    {
                        Conexion.Execute(
                            "UPDATE Tarifa SET Stock = Stock + 1 WHERE IdTarifa = @idtarifa",
                            new { idtarifa = entrada.tarifa?.IdTarifa ?? entrada.tarifa.IdTarifa }
                        );

                        Conexion.Execute(
                            "UPDATE Entrada SET Estado = 'Cancelado' WHERE IdEntrada = @identrada",
                            new { identrada = entrada.IdEntrada }
                        );
                    }
                }

                Conexion.Execute(
                    "UPDATE Evento SET Estado = 'Cancelado' WHERE IdEvento = @idevento",
                        new { idevento = id }
                );

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string PublicarEvento(int id)
        {
            var evento = ObtenerEventoPorID(id);
            if (evento == null)
                throw new ArgumentNullException("El evento no existe");

            if (evento.Estado.ToString().ToLower() == EEstados.Publicado.ToString().ToLower().Trim())
                throw new Exception("El evento ya fue publicado");

            string SqlQuery = @"SELECT * 
                                FROM Funcion f 
                                JOIN Tarifa t USING (IdFuncion) 
                                WHERE f.IdEvento = @IdEvento AND t.Stock > 0";

            var funciones = Conexion.Query<Funcion, Tarifa, FuncionTarifaDTO>(
                SqlQuery,
                (funcion, tarifa) => new FuncionTarifaDTO
                {
                    funcion = funcion,
                    tarifa = tarifa
                },
                new { IdEvento = id },
                splitOn: "IdTarifa"
            );
            if (!funciones.Any())
                throw new Exception("No se puede publicar el evento porque no hay stock");

            var rows = Conexion.Execute(
                "UPDATE Evento SET Estado = @estado WHERE IdEvento = @idEvento",
                new { idEvento = id, estado = EEstados.Publicado.ToString().ToLower().Trim() });

            if (rows > 0)
            {
                evento.Estado = EEstados.Publicado;
                return "El evento se publicó correctamente";
            }
            throw new Exception("No se pudo publicar el evento");
        }
    }
}