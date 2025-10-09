using DevXpert.Modulo3.Conteudo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevXpert.Modulo3.Conteudo.Data.Mapping;

public class CursoMapping : IEntityTypeConfiguration<Curso>
{
    public void Configure(EntityTypeBuilder<Curso> builder)
    {
        builder.ToTable("Cursos");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
               .IsRequired()
               .HasColumnType("varchar(100)");

        builder.Property(c => c.CargaHoraria)
               .IsRequired()
               .HasColumnType("bigint");

        builder.Property(c => c.Ativo)
               .IsRequired()
               .HasColumnType("bit");

        builder.Property(c => c.PermitirMatricula)
               .IsRequired()
               .HasColumnType("bit");

        builder.OwnsOne(c => c.ConteudoProgramatico, cp =>
        {
            cp.Property(c => c.Instrutor)
              .IsRequired()
              .HasColumnType("varchar(100)");

            cp.Property(c => c.Ementa)
              .IsRequired()
              .HasColumnType("varchar(1000)");

            cp.Property(c => c.PublicoAlvo)
              .IsRequired()
              .HasColumnType("varchar(250)");            
        });

        builder.HasIndex(c => c.Nome)
               .IsUnique()
               .HasDatabaseName("UQ_NOME_CURSOS")
               .HasFillFactor(80);
    }
}