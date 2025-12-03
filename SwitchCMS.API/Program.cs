using Microsoft.AspNetCore.Authentication;
using SwitchCMS.API.AppService;
using SwitchCMS.DataBaseContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

builder.Services.AddAuthentication("JWTAuthentication")
    .AddScheme<AuthenticationSchemeOptions, JWTAuthenticationHandler>("JWTAuthentication", null);
DIService.AddAuthorizationPolicies(builder);

DIService.AddSwaggerConfig(builder);
DIService.AddServices(builder);


builder.Services.AddHostedService<CompanyInActiveService>();

builder.Services.AddCors();
// Add services to the container.

builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()||app.Environment.IsProduction())
{
    app.UseSwagger();
    // app.MapOpenApi();   
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
