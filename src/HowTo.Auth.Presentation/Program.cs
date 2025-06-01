using HowTo.Auth.Presentation.Docs;
using HowTo.Auth.Presentation.Extensions;
using HowTo.Auth.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilogWithDefaults();

builder.Services.AddEndpointConfigurations();
builder.Services.AddServiceConfigurations(builder.Configuration);
builder.Services.AddSwaggerConfigurations();
builder.Services.AddIdentityConfigurations();
builder.Services.AddAuthenticationConfigurations(builder.Configuration);
builder.Services.AddCorsConfigurations();
builder.Services.AddHealthCheckConfigurations();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUI();
    app.UseCorsConfigurations();
}

app.UseExceptionHandler(_ => { });
app.UseHealthCheckEndpoints();
app.MapControllers();

await app.RunAsync();
