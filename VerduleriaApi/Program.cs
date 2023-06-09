using Microsoft.EntityFrameworkCore;
using VerduleriaApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VerduleriaContext>(opciones =>
    opciones.UseSqlServer(builder.Configuration.GetConnectionString("VerduleriaContext")));

//Esta configuracion permite referencias circulares
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IRolModel, RolModel>();

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