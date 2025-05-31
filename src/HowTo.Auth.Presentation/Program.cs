using HowTo.Auth.Presentation.Configurations;
using HowTo.Auth.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddLoggerConfigs();

builder.Services.AddControllersConfigs();
builder.Services.AddServiceConfigs(builder.Configuration);
builder.Services.AddSwaggerConfigs();
builder.Services.AddIdentityConfigs(builder.Configuration);
builder.Services.AddCorsConfigs();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

app.UseLoggerConfigs();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfigs();
    app.UseCorsConfigs();
}

app.UseExceptionHandler(_ => { });

await app.RunAsync();

namespace HowTo.Auth.Presentation
{
    public partial class Program
    {
    }
}
