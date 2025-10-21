using DevXpert.Modulo3.ModuloFinanceiro.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevXpert.Modulo3.ModuloFinanceiro.Data.Mappings;

public class PagamentoMapping : IEntityTypeConfiguration<Pagamento>
{
    public void Configure(EntityTypeBuilder<Pagamento> builder)
    {
        builder.ToTable("Pagamentos");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.StatusPagamento)
               .IsRequired()
               .HasColumnType("varchar(20)")
               .HasDefaultValue("AguardandoPagamento")
               .HasConversion<string>();

        builder.OwnsOne(c => c.DadosCartao, dc =>
        {
            dc.Property(c => c.Titular)
              .IsRequired()
              .HasColumnName("Titular")
              .HasColumnType("varchar(100)");

            dc.Property(c => c.NumeroCartao)
              .IsRequired()
              .HasColumnName("NumeroCartao")
              .HasColumnType("varchar(20)");

            dc.Property(c => c.Cvv)
              .IsRequired()
              .HasColumnName("Cvv")
              .HasColumnType("varchar(3)");

            dc.Property(c => c.DataValidade)
              .IsRequired()
              .HasColumnName("DataValidade")
              .HasColumnType("varchar(7)");

            //TODO: 
        });
    }
}
