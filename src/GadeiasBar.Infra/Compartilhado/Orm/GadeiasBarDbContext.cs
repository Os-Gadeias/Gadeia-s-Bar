using System.Reflection;
using GadeiasBar.Dominio.Compartilhado.Identity;
using GadeiasBar.Dominio.Modulos.ModuloConta;
using GadeiasBar.Dominio.Modulos.ModuloGarcom;
using GadeiasBar.Dominio.Modulos.ModuloMesa;
using GadeiasBar.Dominio.Modulos.ModuloPedido;
using GadeiasBar.Dominio.Modulos.ModuloProduto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GadeiasBar.Infra.Compartilhado.Orm;

public sealed class GadeiasBarDbContext(
    DbContextOptions<GadeiasBarDbContext> options,
    IProvedorDeUsuario? userProvider = null
) : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<Mesa> Mesas => Set<Mesa>();
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Garcom> Garcons => Set<Garcom>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        Assembly assembly = typeof(GadeiasBarDbContext).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        if (userProvider is not null)
        {
            modelBuilder.Entity<Mesa>()
                .HasQueryFilter(c => c.UserId == userProvider.Id);

            modelBuilder.Entity<Produto>()
                .HasQueryFilter(c => c.UserId == userProvider.Id);

            modelBuilder.Entity<Garcom>()
                .HasQueryFilter(c => c.UserId == userProvider.Id);

            modelBuilder.Entity<Conta>()
                .HasQueryFilter(c => c.UserId == userProvider!.Id);

            modelBuilder.Entity<Pedido>()
                .HasQueryFilter(p => p.UserId == userProvider.Id);

            base.OnModelCreating(modelBuilder);
        }
    }

    public override int SaveChanges()
    {
        Guid? userId = userProvider?.Id;

        if (!userId.HasValue)
        {
            throw new UnauthorizedAccessException(
                "Não é possível salvar entidades do usuário sem estar autenticado."
            );
        }

        foreach (var entry in ChangeTracker.Entries<IEntidadeDoUsuario>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (entry.Entity.UserId == Guid.Empty)
                    {
                        entry.Property(nameof(IEntidadeDoUsuario.UserId)).CurrentValue = userId.Value;
                    }
                    else if (entry.Entity.UserId != userId.Value)
                    {
                        throw new UnauthorizedAccessException(
                            "Tentativa de criar entidade para outro usuário."
                        );
                    }
                    break;

                case EntityState.Modified:
                    Guid idOriginal = entry.Property(nameof(IEntidadeDoUsuario.UserId)).OriginalValue is Guid originalValue
                        ? originalValue
                        : Guid.Empty;

                    Guid idAtual = entry.Property(nameof(IEntidadeDoUsuario.UserId)).CurrentValue is Guid currentValue
                        ? currentValue
                        : Guid.Empty;

                    if (idOriginal != idAtual)
                    {
                        throw new UnauthorizedAccessException(
                            "Não é permitido alterar o usuário de uma entidade."
                        );
                    }

                    if (idAtual != userId.Value)
                    {
                        throw new UnauthorizedAccessException(
                            "Tentativa de modificar entidade de outro usuário."
                        );
                    }
                    break;

                case EntityState.Deleted:
                    Guid instituicaoOriginal = entry.Property(nameof(IEntidadeDoUsuario.UserId)).OriginalValue is Guid originalDeletedValue
                        ? originalDeletedValue
                        : Guid.Empty;

                    if (instituicaoOriginal != userId.Value)
                    {
                        throw new UnauthorizedAccessException(
                            "Tentativa de excluir entidade de outro usuário."
                        );
                    }
                    break;
            }
        }

        return base.SaveChanges();
    }
}
