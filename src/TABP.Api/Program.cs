using Serilog;
using TABP.Api;
using TABP.Api.RateLimiting;
using TABP.Application;
using TABP.Infrastructure;
using TABP.Infrastructure.Persistence;
using static TABP.Api.RateLimiting.Constants;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

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

app.UseAuthentication();

app.UseAuthorization();

app.UseRateLimiter();

app.MapControllers()
   .RequireRateLimiting(policyName: RateLimiterPolicy);

app.Run();