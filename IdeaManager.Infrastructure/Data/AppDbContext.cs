using Microsoft.EntityFrameworkCore;
using IdeaManager.Core.Entities;
using IdeaManager.Core.Enums;

namespace IdeaManager.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Idea> Ideas { get; set; }
    public DbSet<Project> Projects { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configurar a relação 1:1 entre Idea e Project
        modelBuilder.Entity<Project>()
            .HasOne(p => p.Idea)
            .WithOne(i => i.Project)
            .HasForeignKey<Project>(p => p.IdeaId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Índices para melhor performance
        modelBuilder.Entity<Idea>()
            .HasIndex(i => i.Status);
            
        modelBuilder.Entity<Project>()
            .HasIndex(p => p.Status);
            
        modelBuilder.Entity<Project>()
            .HasIndex(p => p.ProgressPercentage);
    }
}