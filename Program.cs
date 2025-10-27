using APIChat.Data;
using APIChat.Service;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<LoginService>(); 

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