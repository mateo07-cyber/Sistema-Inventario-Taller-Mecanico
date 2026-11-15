
using Microsoft.EntityFrameworkCore;
using SistemaInventarioTallerMecanico.Application.Contract;
using SistemaInventarioTallerMecanico.Application.Services;
using SistemaInventarioTallerMecanico.Domain.Repository;
using SistemaInventarioTallerMecanico.Infrastructure.Context;
using SistemaInventarioTallerMecanico.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Conexión con la base de datos
builder.Services.AddDbContext<TallerMecanicoContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de los servicios de aplicación
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();

// Registro de los repositorios
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
