using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Reflection;
using Todo.App.Contracts;
using Todo.App.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices();

var app = builder.Build();

Configure();

app.Run();

void ConfigureServices()
{
    builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();

    var apiURL = builder.Configuration["APIConfiguration:Url"];
    builder.Services.AddSingleton(new HttpClient
    {
        BaseAddress = new Uri(apiURL)
    });

    builder.Services.AddHttpClient<IClient, Client>(client => client.BaseAddress = new Uri(apiURL));
    builder.Services.AddSingleton<ICategoryDataService, CategoryDataService>();
    builder.Services.AddSingleton<ITodoDataService, TodoDataService>();
}

void Configure()
{
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.MapBlazorHub();
    app.MapFallbackToPage("/_Host");
}