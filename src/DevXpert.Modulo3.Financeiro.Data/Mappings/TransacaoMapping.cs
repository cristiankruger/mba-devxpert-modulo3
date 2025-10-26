using DevXpert.Modulo3.ModuloFinanceiro.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevXpert.Modulo3.ModuloFinanceiro.Data.Mappings;

public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.ToTable("Transacoes");
     
        builder.Ignore(t=>t.Ativo);

        builder.HasKey(t => t.Id);

        builder.Property(t => t.StatusPagamento)
               .IsRequired()
               .HasConversion<string>()
               .HasColumnType("varchar(25)");

        builder.HasOne(t => t.Pagamento)
               .WithOne(t => t.Transacao)
               .HasForeignKey<Transacao>(t => t.PagamentoId);

        builder.Property(t => t.Valor)
               .IsRequired()
               .HasColumnName("Valor")
               .HasColumnType("decimal(18,2)");

        builder.Property(t => t.DataTransacao)
               .IsRequired()
               .HasColumnName("DataTransacao")
               .HasColumnType("DateTime");
    }
}