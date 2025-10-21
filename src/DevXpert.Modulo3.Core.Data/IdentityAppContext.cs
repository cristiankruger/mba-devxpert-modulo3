using DevXpert.Modulo3.Core.Domain;
using DevXpert.Modulo3.Core.Messages;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DevXpert.Modulo3.Core.Data;

public class IdentityAppContext(DbContextOptions<IdentityAppContext> options) : IdentityDbContext(options), IUnitOfWork
{
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Aluno> Alunos{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");

        modelBuilder.Ignore<Event>();

        foreach (var property in modelBuilder.Model.GetEntityTypes()
                                                   .SelectMany(e => e.GetProperties()
                                                   .Where(e => e.ClrType == typeof(string) && e.GetColumnType() is null)))
            property.SetColumnType("varchar(100)");

        foreach (var property in modelBuilder.Model.GetEntityTypes()
                                                   .SelectMany(e => e.GetProperties())
                                                   .Where(e => e.ClrType == typeof(decimal) || e.ClrType == typeof(decimal?)))
            property.SetColumnType("decimal(18,2)");

        foreach (var property in modelBuilder.Model.GetEntityTypes()
                                                   .SelectMany(e => e.GetProperties()
                                                                     .Where(e => e.Name == "DataCadastro")))
            property.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                                                       .SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.NoAction;

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityAppContext).Assembly);

        SeedDatabase.Seed(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> Commit()
    {
        foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
        {
            if (entry.State == EntityState.Added)
                entry.Property("DataCadastro").CurrentValue = DateTime.Now;

            if (entry.State == EntityState.Modified)
                entry.Property("DataCadastro").IsModified = false;
        }

        return await base.SaveChangesAsync() > 0;
    }
}

