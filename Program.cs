using APIChat.Data;
using APIChat.Service;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<ChamadoService>();
builder.Services.AddScoped<UsuarioService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddOpenApi(); 


var app = builder.Build();


if (app.Environment.IsDevelopment())
{

    app.MapOpenApi(); 
    
}

app.UseHttpsRedirection();

app.MapControllers(); 

app.Run();