using GadeiasBar.Dominio.Modulos.ModuloConta;
using GadeiasBar.Dominio.Modulos.ModuloGarcom;
using GadeiasBar.Dominio.Modulos.ModuloMesa;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GadeiasBar.Testes.Unidade.Modulos.ModuloConta;

[TestClass]
public sealed class ContaTests
{
    [TestMethod]
    public void Validar_ContaComDadosValidos_NaoRetornaErros()
    {
        // Arrange
        var garcom = new Garcom();
        var mesa = new Mesa();
        var conta = new Conta("João da Silva", garcom, mesa);

        // Act
        var erros = conta.Validar();

        // Assert
        Assert.AreEqual(0, erros.Count);
    }

    [TestMethod]
    public void ValidarConta_SemNomeCliente_RetornaErro()
    {
        // Arrange
        var garcom = new Garcom();
        var mesa = new Mesa();
        var conta = new Conta("", garcom, mesa); // Nome em branco

        // Act
        var erros = conta.Validar();

        // Assert
        Assert.IsTrue(erros.Contains("O nome do cliente é obrigatório."));
    }

    [TestMethod]
    public void ValidarConta_ComNomeClientePequeno_RetornaErro()
    {
        // Arrange
        var garcom = new Garcom();
        var mesa = new Mesa();
        var conta = new Conta("A", garcom, mesa); // Apenas 1 caractere

        // Act
        var erros = conta.Validar();

        // Assert
        Assert.IsTrue(erros.Contains("O Nome do cliente deve conter entre 2 à 100 caracteres"));
    }

    [TestMethod]
    public void ValidarConta_ComNomeNoLimite_NaoRetornaErro()
    {
        // Arrange
        var garcom = new Garcom();
        var mesa = new Mesa();

        // Criando uma string com exatamente 100 caracteres
        string nomeNoLimite = new string('A', 100);
        var conta = new Conta(nomeNoLimite, garcom, mesa);

        // Act
        var erros = conta.Validar();

        // Assert
        Assert.AreEqual(0, erros.Count);
    }

    [TestMethod]
    public void ValidarConta_SemMesa_RetornaErro()
    {
        // Arrange
        var garcom = new Garcom();
        var conta = new Conta("João da Silva", garcom, null!); // Mesa nula

        // Act
        var erros = conta.Validar();

        // Assert
        Assert.IsTrue(erros.Contains("A mesa é obrigatória."));
    }

    [TestMethod]
    public void ValidarConta_SemGarcom_RetornaErro()
    {
        // Arrange
        var mesa = new Mesa();
        var conta = new Conta("João da Silva", null!, mesa); // Garçom nulo

        // Act
        var erros = conta.Validar();

        // Assert
        Assert.IsTrue(erros.Contains("O garçom é obrigatório."));
    }

    [TestMethod]
    public void Atualizar_ContaPersiste_OsDados()
    {
        // Arrange
        var garcomOriginal = new Garcom();
        var mesaOriginal = new Mesa();
        var conta = new Conta("João", garcomOriginal, mesaOriginal);

        var garcomNovo = new Garcom();
        var mesaNova = new Mesa();
        var contaAtualizada = new Conta("Maria", garcomNovo, mesaNova);

        // Simulando a mudança de status (assumindo que exista outro valor além de Aberta no seu enum)
        // Se o único valor do enum que você testar for Aberta, ele apenas copiará Aberta novamente.
        contaAtualizada.StatusConta = (StatusConta)1;

        // Act
        conta.Atualizar(contaAtualizada);

        // Assert
        Assert.AreEqual("Maria", conta.NomeCliente);
        Assert.AreEqual(garcomNovo, conta.Garcom);
        Assert.AreEqual(mesaNova, conta.Mesa);
        Assert.AreEqual(contaAtualizada.StatusConta, conta.StatusConta);
    }
}