using LMSApp.Application;
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

// ? BULARNI QO'SHING (NSwag metodlari):
app.UseOpenApi(); // OpenAPI spec generatsiya qiladi
app.UseSwaggerUi(settings =>
{
    settings.Path = ""; // Swagger UI asosiy sahifada ochiladi
    settings.DocumentPath = "/swagger/v1/swagger.json";
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
