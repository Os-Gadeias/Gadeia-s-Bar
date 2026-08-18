using GadeiasBar.Dominio.Modulos.ModuloConta;
using GadeiasBar.Dominio.Modulos.ModuloGarcom;
using GadeiasBar.Dominio.Modulos.ModuloMesa;
using GadeiasBar.Testes.Integracao.Compartilhado.Orm;

namespace GadeiasBar.Testes.Integracao.Modulos.ModuloConta;

[TestClass]
public class RepositorioContaOrmTests : RepositorioBaseEmOrmTests
{
    [TestMethod]
    public void CadastrarConta_ComDadosValidos_Persiste()
    {
        Mesa mesa = new();
        Garcom garcom = new();

        Conta conta = new("Thiago", garcom, mesa);

        repositorioConta.Cadastrar(conta);
        dbContext.ChangeTracker.Clear();

        Conta? contaCadastrada = repositorioConta.SelecionarPorId(conta.Id);

        Assert.IsNotNull(contaCadastrada);
        Assert.AreEqual("Thiago", contaCadastrada.NomeCliente);
    }
}
