using NetworkWatcher.Components;
using NetworkWatcher.Services;
using NetworkWatcher.Models;
using NetworkWatcher.DbContexts;
using Serilog;
using System.Text;
using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;

Log.Logger = new LoggerConfiguration()
	.WriteTo.Console()
	.MinimumLevel.Debug()
	.WriteTo.File("logs/connections.txt", rollingInterval: RollingInterval.Day)
	.CreateLogger();

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost.UseUrls("http://192.168.31.102:7130");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<AppDBContext>(
    DbContextOptions => DbContextOptions.UseSqlite(
        builder.Configuration["ConnectionStrings:NetworkWatcherDBConnectionString"]));

builder.Services.AddTransient<INetworkWatcherRepository, NetworkWatcherRepository>();

//builder.Services.AddSignalR();
builder.Services.AddSignalR(HubOptions =>
    {
        HubOptions.ClientTimeoutInterval = TimeSpan.FromSeconds(12);
        HubOptions.KeepAliveInterval = TimeSpan.FromSeconds(6);
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            //builder.WithOrigins("https://example.com")
            builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .WithMethods("GET", "POST");
                //.AllowCredentials();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseCors();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.MapHub<ServiceHub>("/chat");

app.Run();
