using GadeiasBar.Dominio.Modulos.ModuloGarcom;
using GadeiasBar.Dominio.Modulos.ModuloMesa;
using GadeiasBar.Testes.E2E.Compartilhado;
using Microsoft.Playwright;

namespace GadeiasBar.Testes.E2E.Modulos.ModuloConta;

[TestClass]
public class ContaE2ETests : E2ETestsBase
{
    [TestMethod]
    public async Task CadastrarConta_ComDadosValidos_RetornaContaNaListagem()
    {
        await RegistrarEEntrarAsync("Thiago@gmail.com", "Teste@123");

        await RegistrarMesa("45", "10");

        await RegistrarGarcom("Victor");

        await RegistrarConta("Kauazin Silva", "45", "Victor");

        string rotaAbsoluta = new Uri(Page.Url).AbsolutePath;

        Assert.AreEqual("/Conta/Listar", rotaAbsoluta);
        await Expect(Page.GetByText("Kauazin Silva")).ToBeVisibleAsync();
        await Expect(Page.GetByText("Nº da Mesa 45")).ToBeVisibleAsync();
        await Expect(Page.GetByText("Victor")).ToBeVisibleAsync();

    }
    [TestMethod]
    public async Task EditarConta_PersisteOsDados_ERetornaNaListagem()
    {
        await RegistrarEEntrarAsync("Thiago@gmail.com", "Teste@123");

        await RegistrarMesa("45", "9");
        await RegistrarMesa("60", "7");

        await RegistrarGarcom("Thiago Kovalski");
        await RegistrarGarcom("Victor Jeremias");

        await RegistrarConta("Kovalski", "45", "Thiago Kovalski");

        await Page.GetByRole(AriaRole.Link, new() { Name = "Editar" }).ClickAsync();

        await Page.GetByLabel("Nome Cliente").FillAsync("Alexrande Rech");
        await Page.GetByLabel("Selecione a Mesa").SelectOptionAsync("60");
        await Page.GetByLabel("Selecione o Garçom").SelectOptionAsync("Victor Jeremias");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" }).ClickAsync();

        string rotaAbsoluta = new Uri(Page.Url).AbsolutePath;
        Assert.AreEqual("/Conta/Listar", rotaAbsoluta);

        await Expect(Page.GetByText("Alexrande Rech")).ToBeVisibleAsync();
        await Expect(Page.GetByText("Nº da Mesa 60")).ToBeVisibleAsync();
        await Expect(Page.GetByText("Victor Jeremias")).ToBeVisibleAsync();

        await Expect(Page.GetByText("Kovalski")).Not.ToBeVisibleAsync();
        await Expect(Page.GetByText("Nº da Mesa 45")).Not.ToBeVisibleAsync();
        await Expect(Page.GetByText("Thiago Kovalski")).Not.ToBeVisibleAsync();
    }
    private async Task RegistrarConta(string nomeCliente, string mesa, string garcom)
    {
        await Page.GotoAsync(UrlBase + "/Conta/Cadastrar");

        await Page.GetByLabel("Nome Cliente").FillAsync(nomeCliente);
        await Page.GetByLabel("Selecione a Mesa").SelectOptionAsync(mesa);
        await Page.GetByLabel("Selecione o Garçom").SelectOptionAsync(garcom);

        await Page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" }).ClickAsync();
    }
    private async Task RegistrarMesa(string numeroMesa, string qntdLugares)
    {
        await Page.GotoAsync(UrlBase + "/Mesa/Cadastrar");

        await Page.GetByLabel("Numero da mesa").FillAsync(numeroMesa);
        await Page.GetByLabel("Quantidade de Lugares").FillAsync(qntdLugares);

        await Page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" }).ClickAsync();
    }
    private async Task RegistrarGarcom(string nomeGarcom)
    {
        await Page.GotoAsync(UrlBase + "/Garcom/Cadastrar");
        await Page.GetByLabel("Nome").FillAsync(nomeGarcom);
        await Page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" }).ClickAsync();
    }
}
