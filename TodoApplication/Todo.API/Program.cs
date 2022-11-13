using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Todo.API.Middleware;
using Todo.Application;
using Todo.Persistence;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices();

var app = builder.Build();

Configure();

app.Run();

void ConfigureServices()
{
    builder.Services.AddApplicationServices();
    builder.Services.AddPersistenceServices(builder.Configuration);
    builder.Services.AddControllers();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo.API", Version = "v1" });
    });
}

void Configure()
{
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo.API v1"));
    }

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseCustomExceptionHandler();

    app.UseAuthorization();

    app.MapControllers();

    app.AddPersistenceSeedData();
}