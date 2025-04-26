using Microsoft.EntityFrameworkCore;
using Proyecto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Get the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<NotaAcademicaContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

// Map routes
app.MapGet("/", () => "Hello World!");

// Run the application
app.Run();