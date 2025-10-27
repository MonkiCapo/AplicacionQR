
using AppQR.Core.Servicios.Repositorios;
using AppQR.Dapper;
using AppQR.Core.Servicios.Validadores;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using AppQR.Core.Servicios.IServicios;
using AppQR.Services.Servicios;
using AppQR.Core.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using AppQR.Services.Validadores;

var builder = WebApplication.CreateBuilder(args);

var key = builder.Configuration["Jwt:Key"];
var issuer = builder.Configuration["Jwt:Issuer"];

// Validar que la clave JWT tenga longitud suficiente
if (string.IsNullOrEmpty(key) || Encoding.UTF8.GetBytes(key).Length < 32)
{
    throw new ArgumentException("La clave JWT debe tener al menos 32 caracteres (256 bits)");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});
builder.Services.AddAuthorization();

#region Agregando conexión a BD
builder.Services.AddScoped<IDbConnection>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new MySqlConnection(connectionString);
});
#endregion

// builder.Services.AddControllers()
//     .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ClienteFluent>());

builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IRefreshTokenRepositorio, RefreshTokenRepositorio>();
builder.Services.AddScoped<ILocalRepositorio, LocalRepositorio>();
builder.Services.AddScoped<IEventosRepositorio, EventosRepositorio>();
builder.Services.AddScoped<IFuncionRepositorio, FuncionRepositorio>();
builder.Services.AddScoped<ITarifaRepositorio, TarifaRepositorio>();
builder.Services.AddScoped<IOrdenRepositorio, OrdenRepositorio>();
builder.Services.AddScoped<IEntradaRepositorio, EntradaRepositorio>();

builder.Services.AddScoped<ClienteFluent>();
builder.Services.AddScoped<LocalFluent>();
builder.Services.AddScoped<SectorFluent>();

builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<ILocalService, LocalService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "AppQR API", 
        Version = "v1",
        Description = "API para sistema de gestión de entradas QR",
        Contact = new OpenApiContact
        {
            Name = "AppQR Team",
            Email = "soporte@appqr.com"
        }
    });

    // Configuración de seguridad JWT para Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando el esquema Bearer. Ejemplo: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// // Middleware pipeline
// if (app.Environment.IsDevelopment())
// {
//     app.UseDeveloperExceptionPage();
// }

//app.UseRouting();>

//Authentication antes de Authorization
app.UseAuthentication();
app.UseAuthorization();

// Swagger configuration
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AppQR API V1");
    c.RoutePrefix = "swagger"; // http://localhost:5096/swagger
    c.DisplayRequestDuration(); // Muestra el tiempo de respuesta
    c.EnableDeepLinking(); // Permite enlaces directos a endpoints>
});

app.UseHttpsRedirection();

#region EndPoints

//app.MapControllers();
#region CLientes

    app.MapGet("/api/Cliente", (IClienteService service) =>
    {
        var clientes = service.ObtenerClientes();
        return Results.Ok(clientes);
    }).WithTags("Cliente");

    app.MapGet("/api/Cliente/{dni}", (int dni, IClienteService service) =>
    {
        var clientes = service.ObtenerClientePorDNI(dni);
        return clientes is not null ? Results.Ok(clientes) : Results.NotFound();
    }).WithTags("Cliente");

    app.MapPost("/api/Cliente", (int dni, ClienteDTO dto, IClienteService service) =>
    {
        service.AgregarCliente(dto);
        return Results.Created();
    }).WithTags("Cliente");

app.MapPut("/api/Cliente/{dni}", (int dni, IClienteService service, ClienteDTO dto) =>
{
    service.ActualizarCliente(dto, dni);
    return Results.Ok();
}).WithTags("Cliente");

#endregion

#region Locales

app.MapGet("/api/Local", (ILocalService service) =>
{
    var locales = service.ObtenerLocales();
    return Results.Ok(locales);
}).WithTags("Local");

app.MapGet("/api/Local/{id}", (int id, ILocalService service) =>
{
    var local = service.ObtenerLocalPorID(id);
    return local is not null ? Results.Ok(local) : Results.NotFound();
}).WithTags("Local");

app.MapPost("/api/Local", (LocalDTO dto, ILocalService service) =>
{
    service.AgregarLocal(dto);
    return Results.Created();
}).WithTags("Local");

app.MapPut("/api/Local/{id}", (int id, LocalDTO dto, ILocalService service) =>
{
    service.ActualizarLocal(dto, id);
    return Results.Ok();
}).WithTags("Local");

app.MapDelete("/api/Local/{id}", (int id, ILocalService service) =>
{
    service.EliminarLocal(id);
    return Results.Ok();
}).WithTags("Local");



app.MapGet("/api/Local/{idLocal}/Sector", (int idLocal, ILocalService service) =>
{
    var sectores = service.ObtenerSectoresPorLocal(idLocal);
    return Results.Ok(sectores);
}).WithTags("Sector de local");

app.MapGet("/api/Sector/{id}", (int id, ILocalService service) =>
{
    var sector = service.ObtenerSectorPorID(id);
    return sector is not null ? Results.Ok(sector) : Results.NotFound();
}).WithTags("Sector de local");

app.MapPost("/api/Local/{id}/Sector", (int id, SectorDTO dto, ILocalService service) =>
{
    service.AgregarSector(dto, id);
    return Results.Created();
}).WithTags("Sector de local");

app.MapPut("api/Sector/{id}", (int id, SectorDTO dto, ILocalService service) =>
{
    service.ActualizarSector(dto, id);
    return Results.Ok();
}).WithTags("Sector de local");

app.MapDelete("/api/Sector({id}", (int id, ILocalService service) =>
{
    service.EliminarSector(id);
    return Results.Ok();
}).WithTags("Sector de local");



#endregion

#region Funciones

app.MapGet("/api/Funcion", (IFuncionService service) =>
{
    var funciones = service.ObtenerTodasLasFunciones();
    return Results.Ok(funciones);
}).WithTags("Funcion");

app.MapGet("/api/Funcion/{id}", (int id, IFuncionService service) =>
{
    var funcion = service.ObtenerPorID(id);
    return funcion is not null ? Results.Ok(funcion) : Results.NotFound();
}).WithTags("Funcion");

app.MapPost("/api/Funcion", (FuncionDTO dto, IFuncionService service) =>
{
    service.AgregarFuncion(dto);
    return Results.Created();
}).WithTags("Funcion");

app.MapPut("/api/Funcion/{id}", (int id, FuncionDTO dto, IFuncionService service) =>
{
    service.ActualizarFuncion(dto, id);
    return Results.Ok();
}).WithTags("Funcion");

app.MapDelete("/api/Funcion/{id}", (int id, IFuncionService service) =>
{
    service.EliminarFuncion(id);
    return Results.Ok();
}).WithTags("Funcion");

#endregion


#endregion

app.Run();