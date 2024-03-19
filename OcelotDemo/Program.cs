using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Use Ocelot middleware
app.UseOcelot();

// Add middleware to handle 404 errors
app.Use(async (context, next) =>
{
    if (context.Response.StatusCode == 404)
    {
        await context.Response.WriteAsync("404 - Not Found");
    }
    else
    {
        await next();
    }
});

app.Run();
