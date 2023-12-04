using dotnet.Data;
using dotnet.Interface;
using dotnet.Models;
using dotnet.Repository;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<DatabaseInterface, DatabaseRepository>();
builder.Services.AddScoped<GenericQuery<Person>, PersonQueryRepository>();
builder.Services.AddScoped<GenericCommand<Person>, PersonCommandRepository>();
builder.Services.AddScoped<DapperInterface, DatabaseRepositoryDapper>();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<ApplicationContext>
    (
        options => options.UseMySql(
            connectionString, ServerVersion.AutoDetect(connectionString)
            )
        );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
