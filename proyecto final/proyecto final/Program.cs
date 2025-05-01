using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la cadena de conexión a la base de datos en SQL Server
builder.Services.AddDbContext<MovieDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PeliculasDB"))
);

// Agregar otros servicios necesarios para tu aplicación
builder.Services.AddControllers();  // Si estás usando API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // Si estás usando Swagger

var app = builder.Build();

// Configuración de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();  // Para rutas de controlador API

app.Run();
