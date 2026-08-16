using GadeiasBar.Dominio.Modulos.ModuloMesa;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GadeiasBar.Infra.Modulos.ModuloMesa;

public class MesaConfiguration : IEntityTypeConfiguration<Mesa>
{
    public void Configure(EntityTypeBuilder<Mesa> builder)
    {
        builder.ToTable("Mesas");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.NumeroMesa)
            .IsRequired();

        builder.Property(m => m.QuantidadeLugares)
            .IsRequired();

        builder.Property(m => m.statusMesa)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(m => m.UserId)
            .IsRequired();
    }
}
