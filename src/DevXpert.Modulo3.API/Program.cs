using DevXpert.Modulo3.API.Configurations;
using DevXpert.Modulo3.API.Configurations.Swagger;

var builder = WebApplication.CreateBuilder(args);

builder.AddDatabase()
       .AddSettingsConfiguration()
       .AddJWTConfiguration()
       .AddApiBehaviorConfig()
       .ApiVersioningConfig()
       .AddCorsConfig()
       .AddSwaggerConfig()
       .AddIdentityConfig()
       .ConfigureMediatR()
       .ResolveDependecies();

var app = builder.Build();

app.UseApiConfiguration()
   .UseSwaggerConfig()
   .UseEndPointsConfiguration()
   .MigrateDatabase().Wait();

app.Run();

public partial class Program { }