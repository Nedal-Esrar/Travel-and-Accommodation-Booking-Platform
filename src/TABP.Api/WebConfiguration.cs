using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi.Models;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using TABP.Api.Filters;
using TABP.Api.Middlewares;
using TABP.Api.RateLimiting;
using TABP.Api.Services;
using TABP.Domain.Interfaces.Services;

namespace TABP.Api;

public static class WebConfiguration
{
  public static IServiceCollection AddWebComponents(
    this IServiceCollection services)
  {
    services.AddHttpContextAccessor();

    services.AddScoped<IUserContext, UserContext>();
    
    services.AddEndpointsApiExplorer()
      .AddSwagger();

    services.AddApiVersioning();

    services.AddProblemDetails()
      .AddExceptionHandler<GlobalExceptionHandler>();

    services.AddControllers(options => options.Filters.Add<ActivityLogFilter>())
      .AddJsonOptions(options =>
      {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
      });

    services.AddDateOnlyTimeOnlyStringConverters();
    
    services.AddAutoMapper(Assembly.GetExecutingAssembly());

    services.AddFluentValidation();

    services.AddAuthentication();

    services.AddAuthorization();

    services.AddRateLimiting();

    return services;
  }

  private static IServiceCollection AddSwagger(this IServiceCollection services)
  {
    services.AddSwaggerGen(setup =>
    {
      setup.SwaggerDoc("v1", new OpenApiInfo { Title = "Hotel booking API", Version = "v1" });

      var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

      var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

      setup.IncludeXmlComments(xmlCommentsFullPath);

      setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
      });

      setup.AddSecurityRequirement(new OpenApiSecurityRequirement
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
            {
              Type = ReferenceType.SecurityScheme,
              Id = "Bearer"
            }
          },
          Array.Empty<string>()
        }
      });

      setup.UseDateOnlyTimeOnlyStringConverters();
    });

    return services;
  }

  private static IServiceCollection AddApiVersioning(this IServiceCollection services)
  {
    services.AddApiVersioning(setupAction =>
    {
      setupAction.AssumeDefaultVersionWhenUnspecified = true;

      setupAction.DefaultApiVersion = new ApiVersion(1, 0);

      setupAction.ReportApiVersions = true;

      setupAction.ApiVersionReader = new HeaderApiVersionReader("x-api-version");

      setupAction.UnsupportedApiVersionStatusCode = StatusCodes.Status406NotAcceptable;
    }).AddApiExplorer(options => { options.GroupNameFormat = "'v'VVV"; });

    return services;
  }

  private static IServiceCollection AddFluentValidation(this IServiceCollection services)
  {
    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

    services.AddFluentValidationAutoValidation();

    return services;
  }
}