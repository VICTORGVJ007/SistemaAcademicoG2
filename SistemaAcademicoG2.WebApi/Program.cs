using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SistemaAcademicoG2.Infrastructure.Data;
using SistemaAcademicoG2.Domain.Repositories;
using SistemaAcademicoG2.Application.Services;
using SistemaAcademicoG2.Infrastructure.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// ✅ CONFIGURAR JWT
// ==========================================
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

// ==========================================
// ✅ REPOSITORIOS
// ==========================================
builder.Services.AddScoped<INotaRepository, NotaRepository>();
builder.Services.AddScoped<IAsignaturaRepository, AsignaturaRepository>();
builder.Services.AddScoped<IAsistenciaRepository, AsistenciaRepository>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IGradoRepository, GradoRepository>();
builder.Services.AddScoped<IInscripcionRepository, InscripcionRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IGradoAsignaturaRepository, GradoAsignaturaRepository>();

// ==========================================
// ✅ SERVICIOS
// ==========================================
builder.Services.AddScoped<NotaService>();
builder.Services.AddScoped<AsignaturaService>();
builder.Services.AddScoped<AsistenciaService>();
builder.Services.AddScoped<RolService>();
builder.Services.AddScoped<GradoService>();
builder.Services.AddScoped<InscripcionService>();
builder.Services.AddScoped<UsuarioService>();

// ✅ Tu servicio de autenticación
builder.Services.AddScoped<SysProducto.Aplication.Services.AuthService>();

// ==========================================
// ✅ DB CONTEXT
// ==========================================
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("AivenMySql"),
        new MySqlServerVersion(new Version(8, 0, 36)),
        mySqlOptions => mySqlOptions.EnableRetryOnFailure()
    ));

// ==========================================
// ✅ Swagger con Autorización JWT
// ==========================================
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Autorización JWT. Ejemplo: Bearer {token}",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// ==========================================
// ✅ MIDDLEWARES
// ==========================================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ IMPORTANTE: Autenticación antes de Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
