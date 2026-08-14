using AutoMapper;
using GadeiasBar.Aplicacao.Modulos.ModuloProduto.cs;
using Microsoft.AspNetCore.Mvc;

namespace GadeiasBar.WebApp.Modulos.ModuloProduto;

public class ProdutoController(IMapper mapper, ServicoProduto servicoProduto) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarProdutoDto> dtos = servicoProduto.SelecionarTodos();
        List<ListarProdutoViewModel> listarVms = mapper.Map<List<ListarProdutoViewModel>>(dtos);
        return View(listarVms);
    }
}
