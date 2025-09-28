using AppQR.Core.Servicios;
using AppQR.Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IDbConnection>(provider => {
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new MySqlConnection(connectionString);
});

builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
