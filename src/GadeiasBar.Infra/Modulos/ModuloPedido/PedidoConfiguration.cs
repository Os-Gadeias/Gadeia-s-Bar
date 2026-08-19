using GadeiasBar.Dominio.Modulos.ModuloPedido;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedido");

        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Conta)
            .WithMany(c => c.Pedidos)
            .HasForeignKey(p => p.ContaId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Produto)
            .WithMany()
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);


        builder.Property(p => p.Quantidade)
            .IsRequired();

        builder.Property(p => p.UserId)
            .IsRequired();
    }
}
