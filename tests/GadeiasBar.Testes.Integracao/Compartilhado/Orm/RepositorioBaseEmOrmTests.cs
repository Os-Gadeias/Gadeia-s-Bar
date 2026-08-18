using GadeiasBar.Infra.Compartilhado.Orm;
using GadeiasBar.Testes.Integracao.Compartilhado.Identity;
using Microsoft.EntityFrameworkCore;
using FizzWare.NBuilder;
using GadeiasBar.Infra.Modulos.ModuloConta;
using GadeiasBar.Dominio.Modulos.ModuloConta;


namespace GadeiasBar.Testes.Integracao.Compartilhado.Orm;

public abstract class RepositorioBaseEmOrmTests
{
    protected GadeiasBarDbContext dbContext = null!;
    protected RepositorioContaEmOrm repositorioConta = null!;


    // Hooks / Ganchos
    [TestInitialize]
    public void InicializarContexto()
    {
        dbContext = CriarDbContext(Guid.NewGuid());
        repositorioConta = new(dbContext);

        BuilderSetup.SetCreatePersistenceMethod<Conta>(repositorioConta.Cadastrar);
        BuilderSetup.SetCreatePersistenceMethod<IList<Conta>>((Contas) =>
        {
            {
                foreach (Conta c in Contas)
                    repositorioConta.Cadastrar(c);
            }
        });
    }

    [TestCleanup]
    public void DescartarContexto()
    {
        dbContext.Dispose();
    }

    private static GadeiasBarDbContext CriarDbContext(Guid userId)
    {
        DbContextOptions<GadeiasBarDbContext> options =
            new DbContextOptionsBuilder<GadeiasBarDbContext>()
                .UseInMemoryDatabase($"integracao-{Guid.NewGuid():N}")
                .Options;

        return new GadeiasBarDbContext(options, new ProvedorDeUsuarioFake(userId));
    }
}
