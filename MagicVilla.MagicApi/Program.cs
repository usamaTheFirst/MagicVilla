using MagicVilla.MagicApi.Data;
using MagicVilla.MagicApi.Logging;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.
// File("logs/villaLogs.txt", rollingInterval : RollingInterval.Day).CreateLogger();
//builder.Host.UseSerilog();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDbContext<ApplicationDbContext>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString(("ApplicationDbContext"))));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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
