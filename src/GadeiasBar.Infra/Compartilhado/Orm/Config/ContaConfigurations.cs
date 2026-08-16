using GadeiasBar.Dominio.Modulos.ModuloConta;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GadeiasBar.Infra.Compartilhado.Orm.Config;

public class ContaConfigurations : IEntityTypeConfiguration<Conta>
{
    public void Configure(EntityTypeBuilder<Conta> builder)
    {
        builder.HasKey(c => c.Id)
    .HasName("PK_Conta");

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.NomeCliente)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(c => c.Garcom)
            .WithMany()
            .HasForeignKey("FK_Conta_Garco")
            .IsRequired();

        builder.HasOne(c => c.Mesa)
            .WithMany()
            .HasForeignKey("FK_Conta_Mesa")
            .IsRequired();

        builder.Property(c => c.DataDeAbertura)
            .IsRequired();

        builder.Property(c => c.DataDeFechamento)
            .IsRequired(false);
    }
}
