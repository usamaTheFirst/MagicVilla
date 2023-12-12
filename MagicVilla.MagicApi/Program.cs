using MagicVilla.MagicApi;
using MagicVilla.MagicApi.Data;
using MagicVilla.MagicApi.Logging;
using MagicVilla.MagicApi.Repository;
using MagicVilla.MagicApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.
// File("logs/villaLogs.txt", rollingInterval : RollingInterval.Day).CreateLogger();
//builder.Host.UseSerilog();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDbContext<ApplicationDbContext>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString(("ApplicationDbContext"))));
builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddScoped<IVillaNumberRepository, VillaNumberRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ILogging,Logging>();
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
