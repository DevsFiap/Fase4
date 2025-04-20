using Microsoft.EntityFrameworkCore;
using Fase04.Domain.Entities;

namespace Fase04.Infra.Data.Context;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {}

    public AppDbContext()
    {}

    public DbSet<Contato> Contato { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}