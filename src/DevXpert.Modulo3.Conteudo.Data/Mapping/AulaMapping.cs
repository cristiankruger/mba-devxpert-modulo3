using DevXpert.Modulo3.ModuloConteudo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevXpert.Modulo3.ModuloConteudo.Data.Mapping;

public class AulaMapping : IEntityTypeConfiguration<Aula>
{
    public void Configure(EntityTypeBuilder<Aula> builder)
    {
        builder.ToTable("Aulas");

        builder.HasKey(a => a.Id);

        builder.Property(a => a.Conteudo)
               .IsRequired()
               .HasColumnType("varchar(100)");
        
        builder.Property(a => a.Ativo)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(a => a.Material)
               .IsRequired()
               .HasColumnType("varchar(100)");

        builder.HasOne(a=>a.Curso)
               .WithMany(c => c.Aulas)
               .HasForeignKey(a => a.CursoId);

        builder.HasIndex(a => new { a.Conteudo, a.CursoId })
               .IsUnique()
               .HasDatabaseName("UQ_CONTEUDO_CURSO_AULAS")
               .HasFillFactor(80);        
    }
}