using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DevXpert.Modulo3.Core.Domain;

namespace DevXpert.Modulo3.Core.Data.Mapping;

public class AdminMapping : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.ToTable("Admins");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Email)
               .IsRequired()
               .HasColumnType("varchar(100)");

        builder.Property(a => a.Nome)
               .IsRequired()
               .HasColumnType("varchar(100)");

        builder.Property(a => a.Ativo)
               .IsRequired()
               .HasColumnType("bit");
        
        builder.HasIndex(a => a.Email)
               .IsUnique()
               .HasDatabaseName("UQ_EMAIL_ADMIN")
               .HasFillFactor(80);        
    }
}