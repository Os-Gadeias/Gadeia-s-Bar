using GadeiasBar.Dominio.Modulos.ModuloGarcom;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GadeiasBar.Infra.Compartilhado.Orm.Config;

public class GarcomConfigurations : IEntityTypeConfiguration<Garcom>
{
    public void Configure(EntityTypeBuilder<Garcom> builder)
    {
        builder.HasKey(g => g.Id)
        .HasName("PK_Garcom");

        builder.Property(g => g.Id)
            .ValueGeneratedNever();

        builder.Property(g => g.Nome)
            .HasMaxLength(100)
            .IsRequired();
    }
}
