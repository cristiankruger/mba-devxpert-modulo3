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

        builder.Property(a => a.Titulo)
               .IsRequired()
               .HasColumnType("varchar(100)");
        
        builder.Property(a => a.Ativo)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(a => a.Link)
               .IsRequired()
               .HasColumnType("varchar(250)");

        builder.Property(a => a.Duracao)
               .IsRequired()
               .HasColumnType("bigint");

        builder.HasOne(a=>a.Curso)
               .WithMany(c => c.Aulas)
               .HasForeignKey(a => a.CursoId);

        builder.HasIndex(a => new { a.Titulo, a.CursoId })
               .IsUnique()
               .HasDatabaseName("UQ_TITULO_CURSO_AULAS")
               .HasFillFactor(80);        
    }
}