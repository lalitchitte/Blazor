using BlazorProjectServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

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
            builder
                .WithOrigins("https://gaushala-atdbcsargub8b9fu.canadaeast-01.azurewebsites.net")
                .AllowAnyMethod()
                .AllowAnyHeader();
        }
    );
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.MapHealthChecks("/health");

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseAuthorization();

app.UseStaticFiles();
app.MapControllers();

app.MapRazorPages();
app.MapFallbackToFile("index.html");

app.Run();
