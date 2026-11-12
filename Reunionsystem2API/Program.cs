using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Reunionsystem2API.Data;
using Reunionsystem2API.Domain.Interfaces;
using Reunionsystem2API.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Registro de la conexión a SQL Server con EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddSwaggerGen(); builder.Services.AddAutoMapper(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));
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
