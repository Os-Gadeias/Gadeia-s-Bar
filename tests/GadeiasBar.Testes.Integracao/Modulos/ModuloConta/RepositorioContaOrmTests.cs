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
    [TestMethod]
    public void EditarConta_ComDadosValidos_Altera()
    {
        Mesa mesa = new();
        Garcom garcom = new();

        Conta conta = new("Thiago", garcom, mesa);

        repositorioConta.Cadastrar(conta);
        dbContext.ChangeTracker.Clear();

        Conta? contaAtualizada = repositorioConta.SelecionarPorId(conta.Id);

        contaAtualizada!.NomeCliente = "Thiago Kovalski";

        repositorioConta.Editar(conta.Id, contaAtualizada);
        dbContext.ChangeTracker.Clear();

        Conta? contaEditada = repositorioConta.SelecionarPorId(conta.Id);

        Assert.IsNotNull(contaEditada);
        Assert.AreEqual("Thiago Kovalski", contaEditada.NomeCliente);
    }
    [TestMethod]
    public void ExcluirConta_ComContaExistente_Remove()
    {
        Mesa mesa = new();
        Garcom garcom = new();

        Conta conta = new("Thiago", garcom, mesa);

        repositorioConta.Cadastrar(conta);
        dbContext.ChangeTracker.Clear();

        Conta? contaCadastrada = repositorioConta.SelecionarPorId(conta.Id);

        Assert.IsNotNull(contaCadastrada);

        repositorioConta.Excluir(conta.Id);
        dbContext.ChangeTracker.Clear();

        Conta? contaExcluida = repositorioConta.SelecionarPorId(conta.Id);

        Assert.IsNull(contaExcluida);
    }
    [TestMethod]
    public void SelecionarTodos_ComDuasContas_RetornaDuasContas()
    {
        Mesa mesa1 = new();
        Garcom garcom1 = new();

        Mesa mesa2 = new();
        Garcom garcom2 = new();

        Conta conta1 = new("Thiago", garcom1, mesa1);
        Conta conta2 = new("Victor", garcom2, mesa2);

        repositorioConta.Cadastrar(conta1);
        repositorioConta.Cadastrar(conta2);

        dbContext.ChangeTracker.Clear();

        List<Conta> contas = repositorioConta.SelecionarTodos();

        Assert.AreEqual(2, contas.Count);
    }
}
