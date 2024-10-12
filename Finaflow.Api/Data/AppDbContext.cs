using Finaflow.Api.Data.Mappings;
using Finaflow.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Finaflow.Api.Data;

public class AppDbContext : DbContext {
    public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) {}
    public DbSet<Category> Categories {get; set;} = null!;
    public DbSet<Transaction> Transactions {get;set;} = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CategoryMapping());
        modelBuilder.ApplyConfiguration(new TransactionMapping());
    }
}