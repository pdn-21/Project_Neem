using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Services;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load Environment Variables from .env file
Env.Load();

// Add services to the container.
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
//     ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "NEEM API Documentation",
        Version = "v1",
        Description = "API Documentation for NEEM",
        Contact = new OpenApiContact
        {
            Name = "NEEM Development Team",
            Email = "tps.developers2023@gmail.com"
        }
    });
});

// Register DbContext with connection string from .env
var dbConnect = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
if (string.IsNullOrEmpty(dbConnect))
{
    throw new InvalidOperationException("DB_CONNECTION_STRING is not set in the environment variables.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(dbConnect, ServerVersion.AutoDetect(dbConnect)));

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}else
{
    // Use exception handling middleware in production
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();