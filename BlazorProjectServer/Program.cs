using BlazorProjectServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connstr = builder.Configuration.GetConnectionString("Default");

    options.UseMySql(
        connstr,
        new MySqlServerVersion(new Version(8, 0, 39))
            ?? throw new InvalidOperationException("Connection String Not Found!")
    );
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
    );
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseAuthorization();

app.UseStaticFiles();
app.MapControllers();

app.MapRazorPages();
app.MapFallbackToFile("index.html");

app.Run();
