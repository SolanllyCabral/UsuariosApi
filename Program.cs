using Microsoft.EntityFrameworkCore;
using UsuariosAPI.Context;
using UsuariosAPI.Models;
using UsuariosAPI.Services;
using UsuariosAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Base de datos en memoria
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("UsuariosDb"));

// Inyección de dependencias
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

var app = builder.Build();

// Datos iniciales en memoria
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Usuarios.AddRange(
        new Usuario { Nombre = "Ana Martínez", Correo = "anamartinez@gmail.com", FechaDeNacimiento = new DateTime(1995, 4, 12) },
        new Usuario { Nombre = "Carlos López", Correo = "carloslopez@hotmail.com", FechaDeNacimiento = new DateTime(1988, 9, 3) },
        new Usuario { Nombre = "María Rodríguez", Correo = "mariarodriguez@outlook.com", FechaDeNacimiento = new DateTime(2000, 1, 27) }
    );
    context.SaveChanges();
}

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
