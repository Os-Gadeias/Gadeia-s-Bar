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
    public void CadastrarConta_SemMesaNoRepositorio_RetornaErro()
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
}
