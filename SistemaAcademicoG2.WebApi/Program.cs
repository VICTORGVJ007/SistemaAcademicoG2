using Microsoft.EntityFrameworkCore;
using SistemaAcademicoG2.Infrastructure.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SistemaAcademicoG2.Domain.Repositories;
using SistemaAcademicoG2.Application.Services;
using SistemaAcademicoG2.Infrastructure.Repositories;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<INotaRepository  , NotaRepository>();
builder.Services.AddScoped<NotaService>();
builder.Services.AddScoped<IAsignaturaRepository, AsignaturaRepository>();
builder.Services.AddScoped<AsignaturaService>();
builder.Services.AddScoped<IAsistenciaRepository, AsistenciaRepository>();
builder.Services.AddScoped<AsistenciaService>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<RolService>();
builder.Services.AddScoped<IGradoRepository, GradoRepository>();
builder.Services.AddScoped<GradoService>();
builder.Services.AddScoped<IInscripcionRepository, InscripcionRepository>();
builder.Services.AddScoped<InscripcionService>();
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("AivenMySql"),
    new MySqlServerVersion(new Version(8, 0, 36)),
    mySqlOptions => mySqlOptions.EnableRetryOnFailure()));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
