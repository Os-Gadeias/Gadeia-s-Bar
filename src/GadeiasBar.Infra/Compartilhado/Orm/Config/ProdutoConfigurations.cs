using GadeiasBar.Dominio.Modulos.ModuloProduto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GadeiasBar.Infra.Compartilhado.Orm.Config;

public class ProdutoConfigurations : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.ToTable("TB_Produto");

        builder.HasKey(p => p.Id)
        .HasName("PK_Produto");

        builder.Property(p => p.Id)
        .ValueGeneratedNever();

        builder.Property(p => p.Nome)
        .IsRequired()
        .HasMaxLength(100);

        builder.Property(p => p.TipoProduto)
        .IsRequired();

        builder.Property(p => p.Valor)
        .IsRequired().HasPrecision(6, 2);
    }
}
