using System.Reflection;
using GadeiasBar.Dominio.Compartilhado.Identity;
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
    public DbSet<Produto> Produtos => Set<Produto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        Assembly assembly = typeof(GadeiasBarDbContext).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);

        // Query Filters devem utilizar a dependência do UserProvider diretamente
        // O EF faz cachê do OnModelCreating e variáveis locais não são atualizadas
        if (userProvider is not null)
        {
            modelBuilder.Entity<Produto>()
                .HasQueryFilter(c => c.UserId == userProvider!.Id);

            base.OnModelCreating(modelBuilder);
        }
    }

    public override int SaveChanges()
    {
        Guid? userId = userProvider?.Id;

        if (!userId.HasValue)
        {
            throw new UnauthorizedAccessException(
                "Não é possivel salvar entidades da instituicao sem estar autenticado!"
            );
        }

        foreach (var entry in ChangeTracker.Entries<IEntidadeDoUsuario>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    if (entry.Entity.UserId == Guid.Empty)
                    {
                        entry.Property(nameof(IEntidadeDoUsuario.UserId)).CurrentValue = userId;
                    }
                    else if (entry.Entity.UserId != userId)
                    {
                        throw new UnauthorizedAccessException(
                            "Tentativa de Criar entidade para outra instituição!"
                         );
                    }
                    break;

                case EntityState.Modified:

                    Guid idInstituicaoOriginal = entry
                    .Property(nameof(IEntidadeDoUsuario.UserId))
                    .OriginalValue is Guid originalGuid
                    ? originalGuid : Guid.Empty;

                    Guid idAtualIntituicao = entry.
                    Property(nameof(IEntidadeDoUsuario.UserId))
                    .OriginalValue is Guid idAtual
                    ? idAtual : Guid.Empty;

                    if (idAtualIntituicao != idInstituicaoOriginal)
                    {
                        throw new UnauthorizedAccessException(
                            "Não é permitido alterar a instituição de uma entidade!"
                         );
                    }

                    if (idAtualIntituicao != userId)
                    {
                        throw new UnauthorizedAccessException(
                            "Tentativa de Criar entidade para outra instituição!"
                         );
                    }
                    break;

                case EntityState.Deleted:

                    Guid InstituicaoOriginal = entry
                   .Property(nameof(IEntidadeDoUsuario.UserId))
                   .OriginalValue is Guid original
                   ? original : Guid.Empty;

                    if (InstituicaoOriginal != userId.Value)
                    {
                        throw new UnauthorizedAccessException(
                            "Tentativa de Criar entidade para outra instituição!"
                         );
                    }

                    break;
            }
        }

        return base.SaveChanges();
    }
}
