using Microsoft.EntityFrameworkCore;
using CyberGuard.Domain;

namespace CyberGuard.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }

    // On dit à EF Core de créer une table "Vulnerabilities" basée sur notre modèle Domain
    public DbSet<Vulnerability> Vulnerabilities { get; set; }
}