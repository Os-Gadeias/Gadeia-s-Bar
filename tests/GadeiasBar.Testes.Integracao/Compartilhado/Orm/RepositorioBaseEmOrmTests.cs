using GadeiasBar.Infra.Compartilhado.Orm;
using GadeiasBar.Testes.Integracao.Compartilhado.Identity;
using Microsoft.EntityFrameworkCore;
using FizzWare.NBuilder;


namespace GadeiasBar.Testes.Integracao.Compartilhado.Orm;

public abstract class RepositorioBaseEmOrmTests
{
    protected GadeiasBarDbContext dbContext = null!;
    // protected RepositorioDisciplinaEmOrm repositorioDisciplina = null!;


    // Hooks / Ganchos
    [TestInitialize]
    public void InicializarContexto()
    {
        dbContext = CriarDbContext(Guid.NewGuid());

        // Disciplina
        // repositorioDisciplina = new RepositorioDisciplinaEmOrm(dbContext);

        // BuilderSetup.SetCreatePersistenceMethod<Disciplina>(repositorioDisciplina.Cadastrar);
        // BuilderSetup.SetCreatePersistenceMethod<IList<Disciplina>>((disciplinas) =>
        // {
        //     foreach (Disciplina d in disciplinas)
        //         repositorioDisciplina.Cadastrar(d);
        // });

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
