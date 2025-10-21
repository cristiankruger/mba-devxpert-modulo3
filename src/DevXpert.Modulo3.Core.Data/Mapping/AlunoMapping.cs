using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DevXpert.Modulo3.Core.Domain;

namespace DevXpert.Modulo3.Core.Data.Mapping;

public class AlunoMapping : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.ToTable("Alunos");

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
               .HasDatabaseName("UQ_EMAIL_ALUNO")
               .HasFillFactor(80);        
    }
}