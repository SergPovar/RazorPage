using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using RazorPage.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
 builder.Services.AddDbContext<AppDbContext>( optionsBuilder =>
 {
     optionsBuilder.UseMySql( "server=localhost; user = serg; password = 12345; database= RazorPage_DB;"
         ,
         new MySqlServerVersion(new Version(8, 0, 34)));
});
 builder.Services.AddScoped<IEmployeeRepository, MySQLEmployeeRepository>();
// builder.Services.AddSingleton<IEmployeeRepository, MokEmployeeRepository>();
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
    options.AppendTrailingSlash = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
