using Serilog;
using TABP.Api;
using TABP.Application;
using TABP.Infrastructure;
using TABP.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => { configuration.ReadFrom.Configuration(context.Configuration); });

builder.Services
  .AddWebComponents()
  .AddApplication()
  .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();

app.UseSwagger();

app.UseSwaggerUI();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.Migrate();

app.UseCors();

app.UseRateLimiter();

app.MapControllers()
  .RequireRateLimiting("FixedWindow");

app.Run();