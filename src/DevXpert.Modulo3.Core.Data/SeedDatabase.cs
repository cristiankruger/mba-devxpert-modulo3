using DevXpert.Modulo3.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DevXpert.Modulo3.Core.Data;

public static class SeedDatabase
{
    public static void Seed(ModelBuilder builder)
    {
        var adminId = Guid.Parse("f96e5735-7f8a-49a7-8fe1-64304e70257b");
        var alunoId = Guid.Parse("f96e5735-7f8a-49a7-8fe1-64304e70257d");

        var senha = "AQAAAAIAAYagAAAAEB1kPW44o68VpBeoDRUByh20VsgylM2MkdGJ9kzepRkS0wkgOqDnahg5xEkN++ogbg ==";//@Aa12345

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "2c5e174e-3b0e-446f-86af-483d56fd7210" },
            new IdentityRole { Id = "2", Name = "Aluno", NormalizedName = "ALUNO", ConcurrencyStamp = "bd1f5f5b-77e4-4ac3-b101-1f3053f4ee6c" }
        );

        builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = alunoId.ToString(),
                    UserName = "aluno@teste.com",
                    NormalizedUserName = "ALUNO@TESTE.COM",
                    NormalizedEmail = "ALUNO@TESTE.COM",
                    Email = "aluno@teste.com",
                    PasswordHash = senha,
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    ConcurrencyStamp = "f1aef7e9-db61-4442-a01a-ea58d7609d21",
                    SecurityStamp = "fdb857cc-1f49-484f-bd6b-bfbba7fedfab"
                },
                new IdentityUser
                {
                    Id = adminId.ToString(),
                    UserName = "admin@teste.com",
                    NormalizedUserName = "ADMIN@TESTE.COM",
                    NormalizedEmail = "ADMIN@TESTE.COM",
                    Email = "admin@teste.com",
                    PasswordHash = senha,
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    ConcurrencyStamp = "f1aef7e9-db61-4442-a01a-ea58d7609d21",
                    SecurityStamp = "fdb857cc-1f49-484f-bd6b-bfbba7fedfab"
                }
            );

        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = adminId.ToString(), RoleId = "1" },
            new IdentityUserRole<string> { UserId = alunoId.ToString(), RoleId = "2" }
        );

        builder.Entity<Aluno>().HasData(
            new Aluno(alunoId, "aluno@teste.com", "aluno@teste.com")
        );

        builder.Entity<Admin>().HasData(
            new Admin(adminId, "admin@teste.com", "admin@teste.com")
        );
    }
}
