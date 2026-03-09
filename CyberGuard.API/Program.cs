using Microsoft.EntityFrameworkCore;
using CyberGuard.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// --- 1. CONFIGURATION DES SERVICES ---
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ICI : On crée la règle "Autoriser Tout"
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- 2. CONFIGURATION DU PIPELINE (L'ordre compte !) ---

// ICI : On active la règle AVANT les contrôleurs
app.UseCors("AllowAll"); 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// On commente cette ligne si elle existe pour éviter les conflits de ports HTTPS
// app.UseHttpsRedirection(); 

app.MapControllers();

app.Run();