using AnalyticsServer.Cache;
using AnalyticsServer.Contracts;
using AnalyticsServer.Models;
using AnalyticsServer.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();

builder.Host.UseSerilog((hostContext, _, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration);
});

var configuration = builder.Configuration.GetSection("ConfigurationService").Get<ConfigurationService>()!;


builder.Services.AddSingleton(configuration);

builder.Services.AddCache(configuration.RedisConnectionString);
builder.Services.AddSingleton<IAnalyticsRepository, AnalyticsRepository>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();