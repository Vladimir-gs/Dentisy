using Microsoft.EntityFrameworkCore;
using Server.Context;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Configuracion de la conexion a la base de datos
var ConStr = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<DentisyContext>(options =>
    options.UseSqlServer(ConStr));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();