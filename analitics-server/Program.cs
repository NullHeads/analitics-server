using AnalyticsServer.Contracts;
using AnalyticsServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAnalyticsRepository, AnalyticsRepositoryMock>();
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