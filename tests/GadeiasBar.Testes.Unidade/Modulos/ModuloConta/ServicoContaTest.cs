using FluentResults;
using GadeiasBar.Aplicacao.Modulos.ModuloConta;
using GadeiasBar.Dominio.Modulos.ModuloConta;
using GadeiasBar.Dominio.Modulos.ModuloGarcom;
using GadeiasBar.Dominio.Modulos.ModuloMesa;
using Moq;

namespace GadeiasBar.Testes.Unidade.Modulos.ModuloConta;

[TestClass]
public class ServicoContaTest
{
    [TestMethod]
    public void CadastrarConta_ComDadosValidos_NaoRetornaErro()
    {
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioConta> repositorioConta = new();
        Mock<IRepositorioGarcom> repositorioGarcom = new();

        ServicoConta servicoConta = new(
            repositorioConta.Object,
            repositorioMesa.Object,
            repositorioGarcom.Object);

        Mesa mesa = new();
        Garcom garcom = new();

        repositorioMesa.Setup(r => r.SelecionarPorId(It.IsAny<Guid>())).Returns(mesa);
        repositorioGarcom.Setup(r => r.SelecionarPorId(It.IsAny<Guid>())).Returns(garcom);

        Conta? contaCadastrada = null!;
        repositorioConta.Setup(c => c.Cadastrar(It.IsAny<Conta>())).Callback<Conta>(conta => contaCadastrada = conta);

        Result resultado = servicoConta.Cadastrar(
            new("Thiago",
            mesa.Id.ToString(),
            garcom.Id.ToString()));

        Assert.IsTrue(resultado.IsSuccess);
        Assert.IsNotNull(contaCadastrada);
        repositorioConta.Verify(r => r.Cadastrar(It.IsAny<Conta>()), Times.Once);
    }
    [TestMethod]
    public void CadastrarConta_SemNomeClienteValido_RetornaErro()
    {
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioConta> repositorioConta = new();
        Mock<IRepositorioGarcom> repositorioGarcom = new();

        ServicoConta servicoConta = new(
            repositorioConta.Object,
            repositorioMesa.Object,
            repositorioGarcom.Object);

        Mesa mesa = new();
        Garcom garcom = new();

        repositorioMesa.Setup(r => r.SelecionarPorId(It.IsAny<Guid>())).Returns(mesa);
        repositorioGarcom.Setup(r => r.SelecionarPorId(It.IsAny<Guid>())).Returns(garcom);

        Conta? contaCadastrada = null!;
        repositorioConta.Setup(c => c.Cadastrar(It.IsAny<Conta>())).Callback<Conta>(conta => contaCadastrada = conta);

        Result resultado = servicoConta.Cadastrar(
            new(string.Empty,
            mesa.Id.ToString(),
            garcom.Id.ToString()));

        Assert.IsTrue(resultado.IsFailed);
        Assert.IsNull(contaCadastrada);
        repositorioConta.Verify(r => r.Cadastrar(It.IsAny<Conta>()), Times.Never);
    }
    [TestMethod]
    public void CadastrarConta_SemMesaCadastradoNoRepositorio_RetornaErro()
    {
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioConta> repositorioConta = new();
        Mock<IRepositorioGarcom> repositorioGarcom = new();

        ServicoConta servicoConta = new(
            repositorioConta.Object,
            repositorioMesa.Object,
            repositorioGarcom.Object);

        Garcom garcom = new();
        Mesa mesa = new();
        repositorioMesa.Setup(r => r.SelecionarPorId(It.IsAny<Guid>())).Returns<Mesa?>(null!);
        repositorioGarcom.Setup(r => r.SelecionarPorId(It.IsAny<Guid>())).Returns(garcom);

        Conta? contaCadastrada = null!;
        repositorioConta.Setup(c => c.Cadastrar(It.IsAny<Conta>())).Callback<Conta>(conta => contaCadastrada = conta);

        Result resultado = servicoConta.Cadastrar(
            new("Thiago",
            mesa.Id.ToString(),
            garcom.Id.ToString()));

        Assert.IsTrue(resultado.IsFailed);
        Assert.Contains("Mesa não", resultado.Errors.First().Message);
        Assert.IsNull(contaCadastrada);
        repositorioConta.Verify(r => r.Cadastrar(It.IsAny<Conta>()), Times.Never);
    }
    [TestMethod]
    public void CadastrarConta_SemGarcomCadastradoNoRepositorio_RetornaErro()
    {
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioConta> repositorioConta = new();
        Mock<IRepositorioGarcom> repositorioGarcom = new();

        ServicoConta servicoConta = new(
            repositorioConta.Object,
            repositorioMesa.Object,
            repositorioGarcom.Object);

        Garcom garcom = new();
        Mesa mesa = new();
        repositorioMesa.Setup(r => r.SelecionarPorId(It.IsAny<Guid>())).Returns(mesa);
        repositorioGarcom.Setup(r => r.SelecionarPorId(It.IsAny<Guid>())).Returns<Garcom?>(null!);

        Conta? contaCadastrada = null!;
        repositorioConta.Setup(c => c.Cadastrar(It.IsAny<Conta>())).Callback<Conta>(conta => contaCadastrada = conta);

        Result resultado = servicoConta.Cadastrar(
            new("Thiago",
            mesa.Id.ToString(),
            garcom.Id.ToString()));

        Assert.IsTrue(resultado.IsFailed);
        Assert.Contains("Garçom não", resultado.Errors.First().Message);
        Assert.IsNull(contaCadastrada);
        repositorioConta.Verify(r => r.Cadastrar(It.IsAny<Conta>()), Times.Never);
    }
    [TestMethod]
    public void EditarConta_ComDadosValidos_Persiste()
    {
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioConta> repositorioConta = new();
        Mock<IRepositorioGarcom> repositorioGarcom = new();

        ServicoConta servicoConta = new(
            repositorioConta.Object,
            repositorioMesa.Object,
            repositorioGarcom.Object);

        Garcom garcom = new("Kauazin");
        Mesa mesa = new();
        Conta conta = new("Thiago Kovalski", garcom, mesa);

        Garcom garcom2 = new("Victor");
        Mesa mesa2 = new();

        repositorioMesa.Setup(r => r.SelecionarPorId(It.IsAny<Guid>())).Returns(mesa2);
        repositorioGarcom.Setup(r => r.SelecionarPorId(It.IsAny<Guid>())).Returns(garcom2);
        repositorioConta.Setup(c => c.SelecionarPorId(It.IsAny<Guid>())).Returns(conta);

        repositorioConta.Setup(c => c.Editar(It.IsAny<Guid>(), It.IsAny<Conta>())).Returns(true);

        Result resultado = servicoConta.Editar(
            new(conta.Id, "Tiago Santini", garcom2.Id, mesa2.Id));

        Assert.IsTrue(resultado.IsSuccess);
        repositorioConta.Verify(r => r.Editar(It.IsAny<Guid>(), It.IsAny<Conta>()), Times.Once);
    }
    [TestMethod]
    public void CadastrarConta_ComDadosValidos_RetornaMesaOcupada()
    {
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioConta> repositorioConta = new();
        Mock<IRepositorioGarcom> repositorioGarcom = new();

        ServicoConta servicoConta = new(
            repositorioConta.Object,
            repositorioMesa.Object,
            repositorioGarcom.Object);

        Mesa mesa = new();
        Garcom garcom = new();

        repositorioMesa.Setup(r => r.SelecionarPorId(It.IsAny<Guid>())).Returns(mesa);
        repositorioGarcom.Setup(r => r.SelecionarPorId(It.IsAny<Guid>())).Returns(garcom);

        Conta? contaCadastrada = null!;
        repositorioConta.Setup(c => c.Cadastrar(It.IsAny<Conta>())).Callback<Conta>(conta => contaCadastrada = conta);

        Result resultado = servicoConta.Cadastrar(
            new("Thiago",
            mesa.Id.ToString(),
            garcom.Id.ToString()));

        Assert.IsTrue(resultado.IsSuccess);
        Assert.IsTrue(contaCadastrada.Mesa.statusMesa == StatusMesa.Ocupada);
        repositorioConta.Verify(r => r.Cadastrar(It.IsAny<Conta>()), Times.Once);
    }
    [TestMethod]
    public void ExcluirConta_ComContaExistente_NaoRetornaErro()
    {
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioConta> repositorioConta = new();
        Mock<IRepositorioGarcom> repositorioGarcom = new();

        ServicoConta servicoConta = new(
            repositorioConta.Object,
            repositorioMesa.Object,
            repositorioGarcom.Object);

        Mesa mesa = new();
        Garcom garcom = new();

        Conta conta = new(
            "Thiago",
            garcom,
            mesa
        );

        repositorioConta
            .Setup(r => r.SelecionarPorId(It.IsAny<Guid>()))
            .Returns(conta);

        repositorioMesa.Setup(r => r.SelecionarPorId(It.IsAny<Guid>())).Returns(mesa);
        repositorioGarcom.Setup(r => r.SelecionarPorId(It.IsAny<Guid>())).Returns(garcom);

        Result resultado = servicoConta.Excluir(new(conta.Id, conta.NomeCliente, "Thiago", mesa.Id, 23, "", "", StatusConta.Fechada, 200));

        Assert.IsTrue(resultado.IsSuccess);

        repositorioConta.Verify(
            r => r.Excluir(It.IsAny<Guid>()),
            Times.Once);
    }
    [TestMethod]
    public void ExcluirConta_ComContaExistente_RetornaMesa_Desocupada()
    {
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioConta> repositorioConta = new();
        Mock<IRepositorioGarcom> repositorioGarcom = new();

        ServicoConta servicoConta = new(
            repositorioConta.Object,
            repositorioMesa.Object,
            repositorioGarcom.Object);

        Mesa mesa = new();
        mesa.OcuparMesa(true);

        Garcom garcom = new();

        Conta conta = new(
            "Thiago",
            garcom,
            mesa
        );

        repositorioConta
            .Setup(r => r.SelecionarPorId(It.IsAny<Guid>()))
            .Returns(conta);

        repositorioMesa.Setup(r => r.SelecionarPorId(It.IsAny<Guid>())).Returns(mesa);
        repositorioGarcom.Setup(r => r.SelecionarPorId(It.IsAny<Guid>())).Returns(garcom);

        Result resultado = servicoConta.Excluir(new(conta.Id, conta.NomeCliente, "Thiago", mesa.Id, 23, "", "", StatusConta.Fechada, 200));

        Assert.IsTrue(resultado.IsSuccess);
        Assert.IsTrue(mesa.statusMesa == StatusMesa.Livre);
        repositorioConta.Verify(
            r => r.Excluir(It.IsAny<Guid>()),
            Times.Once);
    }
    [TestMethod]
    public void EditarConta_ComOutraMesa_DeixaAMesaAntiga_Livre_E_OcupaANova()
    {
        Mock<IRepositorioMesa> repositorioMesa = new();
        Mock<IRepositorioConta> repositorioConta = new();
        Mock<IRepositorioGarcom> repositorioGarcom = new();

        ServicoConta servicoConta = new(
            repositorioConta.Object,
            repositorioMesa.Object,
            repositorioGarcom.Object);

        Garcom garcom = new("Kauazin");
        Mesa mesaAntiga = new();
        mesaAntiga.OcuparMesa(true);
        Conta conta = new("Thiago Kovalski", garcom, mesaAntiga);

        Garcom garcom2 = new("Victor");
        Mesa mesaNova = new();

        repositorioMesa.Setup(r => r.SelecionarPorId(It.IsAny<Guid>())).Returns(mesaNova);
        repositorioGarcom.Setup(r => r.SelecionarPorId(It.IsAny<Guid>())).Returns(garcom2);
        repositorioConta.Setup(c => c.SelecionarPorId(It.IsAny<Guid>())).Returns(conta);

        repositorioConta.Setup(c => c.Editar(It.IsAny<Guid>(), It.IsAny<Conta>())).Returns(true);

        Result resultado = servicoConta.Editar(
            new(conta.Id, "Tiago Santini", garcom2.Id, mesaNova.Id));

        Assert.IsTrue(resultado.IsSuccess);
        Assert.IsTrue(mesaAntiga.statusMesa == StatusMesa.Livre);
        Assert.IsTrue(mesaNova.statusMesa == StatusMesa.Ocupada);
        repositorioConta.Verify(r => r.Editar(It.IsAny<Guid>(), It.IsAny<Conta>()), Times.Once);
    }
}
