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

        await RegistrarMesa();

        await RegistrarGarcom();

        await Page.GotoAsync(UrlBase + "/Cadastrar/Conta");

        await Page.GetByLabel("Nome Cliente").FillAsync("Kauazin Silva");
        await Page.GetByLabel("Selecione a Mesa").SelectOptionAsync("45");
        await Page.GetByLabel("Selecione o Garçom").SelectOptionAsync("Thiago");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" }).ClickAsync();

        string rotaAbsoluta = new Uri(Page.Url).AbsolutePath;

        Assert.AreEqual("/Conta/Listar", rotaAbsoluta);
        await Expect(Page.GetByText("Kauazin Silva")).ToBeVisibleAsync();
        await Expect(Page.GetByText("45")).ToBeVisibleAsync();
        await Expect(Page.GetByText("Thiago")).ToBeVisibleAsync();

    }
    private async Task RegistrarMesa()
    {
        await Page.GotoAsync(UrlBase + "/Mesa/Cadastrar");

        await Page.GetByLabel("Numero da mesa").FillAsync("45");
        await Page.GetByLabel("Quantidade de Lugares").FillAsync("10");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" }).ClickAsync();
    }
    private async Task RegistrarGarcom()
    {
        await Page.GotoAsync(UrlBase + "/Garcom/Cadastrar");
        await Page.GetByLabel("Nome").FillAsync("Thiago");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" }).ClickAsync();
    }
}
