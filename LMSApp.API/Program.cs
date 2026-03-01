using LMSApp.Application;
using LMSApp.Application.Middlewares;
using LMSApp.Domain.Models.JWT;
using LMSApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services
    .Configure<JWTSetting>(builder.Configuration)
    .AddInfrastructureSetting(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddSwaggerServices(builder.Configuration);

var app = builder.Build().MainConfiguration();

// Configure the HTTP request pipeline.

app.UseOpenApi();
app.UseSwaggerUi();

app.UseHttpsRedirection();

app.UseAuthentication();
app.CustomTokenValidationMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();
