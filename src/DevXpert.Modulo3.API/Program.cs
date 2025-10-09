using DevXpert.Modulo3.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.AddApiBehaviorConfig()
       .AddDatabase()
       .AddSettingsConfiguration()
       .AddCorsConfig()
       .AddJWTConfiguration()
       .AddSwaggerConfig()
       .AddIdentityConfig()
       .ResolveDependecies();

var app = builder.Build();

app.UseApiConfiguration()
   .UseSwaggerConfig()
   .UseEndPointsConfiguration()
   .MigrateDatabase().Wait();

app.Run();