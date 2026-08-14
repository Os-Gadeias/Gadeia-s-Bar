using AutoMapper;
using FluentResults;
using GadeiasBar.Aplicacao.Modulos.ModuloProduto.cs;
using GadeiasBar.WebApp.Compartilhado.Extensions;
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
    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }
    [HttpPost]
    public ActionResult Cadastrar(CadastrarProdutoViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        CadastrarProdutoDto dto = mapper.Map<CadastrarProdutoDto>(vm);
        Result resultado = servicoProduto.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return View(vm);
        }
        return RedirectToAction(nameof(Listar));
    }
}
