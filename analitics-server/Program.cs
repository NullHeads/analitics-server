using AnalyticsServer.Cache;
using AnalyticsServer.Contracts;
using AnalyticsServer.Models;
using AnalyticsServer.Schedule;
using AnalyticsServer.Services;
using RabbitMQ.Client;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Host.UseSerilog((hostContext, _, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostContext.Configuration);
});

var configuration = builder.Configuration.GetSection("ConfigurationService").Get<ConfigurationService>()!;

var factory = new ConnectionFactory
{
    HostName = configuration.RabbitMqConfiguration.Host,
    UserName = configuration.RabbitMqConfiguration.Username,
    Password = configuration.RabbitMqConfiguration.Password,
    DispatchConsumersAsync = true
};
builder.Services.AddSingleton(factory);

builder.Services.AddSingleton(configuration);

builder.Services.AddCache(configuration.RedisConnectionString);
builder.Services.AddSingleton<IAnalyticsControllerHandler, AnalyticsControllerHandler>();
builder.Services.AddSingleton<IUserControllerHandler, UserControllerHandler>();

builder.Services.AddSingleton<IAnalyticsRepository, AnalyticsRepository>();
builder.Services.AddSingleton<IAnalyticsSendingService, AnalyticsSendingService>();
builder.Services.AddSingleton<IAnalyticsReceivingService, AnalyticsConsumerService>();

builder.Services.AddControllers();
builder.Services.AddHostedService<AnalyticsServer.Schedule.AnalyticsServer>();
builder.Services.AddHostedService<ConsumerService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(myAllowSpecificOrigins,
        corsPolicyBuilder =>
        {
            corsPolicyBuilder
                .AllowAnyOrigin() 
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(myAllowSpecificOrigins);
app.UseCors(corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();