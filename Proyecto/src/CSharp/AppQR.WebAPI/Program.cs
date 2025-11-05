
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
using AppQR.WebAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

#region Auth JWT

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
builder.Services.AddScoped<RefreshTokenService>();
builder.Services.AddScoped<AuthService>();
    
#endregion

#region Agregando conexión a BD
builder.Services.AddScoped<IDbConnection>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    return new MySqlConnection(connectionString);
});
#endregion

// builder.Services.AddControllers()
//     .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ClienteFluent>());
#region Repositorios
builder.Services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IRefreshTokenRepositorio, RefreshTokenRepositorio>();
builder.Services.AddScoped<ILocalRepositorio, LocalRepositorio>();
builder.Services.AddScoped<IEventosRepositorio, EventosRepositorio>();
builder.Services.AddScoped<IFuncionRepositorio, FuncionRepositorio>();
builder.Services.AddScoped<ITarifaRepositorio, TarifaRepositorio>();
builder.Services.AddScoped<IOrdenRepositorio, OrdenRepositorio>();
builder.Services.AddScoped<IEntradaRepositorio, EntradaRepositorio>();
#endregion

#region Validadores
builder.Services.AddScoped<ClienteFluent>();
builder.Services.AddScoped<EventoFluent>();
builder.Services.AddScoped<LocalFluent>();
builder.Services.AddScoped<SectorFluent>();
builder.Services.AddScoped<FuncionFluent>();
builder.Services.AddScoped<TarifaFluent>();
builder.Services.AddScoped<OrdenFluent>();
builder.Services.AddScoped<UsuarioFluent>();
builder.Services.AddScoped<LoginFluent>();
#endregion

#region Servicios
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IEventoService, EventoService>();
builder.Services.AddScoped<ILocalService, LocalService>();
builder.Services.AddScoped<IFuncionService, FuncionService>();
builder.Services.AddScoped<ITarifaService, TarifaService>();
builder.Services.AddScoped<IOrdenService, OrdenService>();
builder.Services.AddScoped<IEntradaService, EntradaService>();
#endregion

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

//app.MapControllers();
#region EndPoints

app.MapClienteEndpoints();

app.MapEventoEndpoints();

app.MapLocalEndpoints();

app.MapSectorEndpoints();

app.MapFuncionEndpoints();

app.MapTarifaEndpoints();

app.MapOrdenEndpoints();

app.MapEntradaEndpoints();

app.MapAuthEndpoint();

#endregion

app.Run();