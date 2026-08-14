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
    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<ListarProdutoDto> resultado = servicoProduto.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        ExcluirProdutoViewModel vm = mapper.Map<ExcluirProdutoViewModel>(resultado.Value);

        return View(vm);
    }
    [HttpPost]
    public ActionResult Excluir(ExcluirProdutoViewModel vm)
    {
        ExcluirProdutoDto dto = mapper.Map<ExcluirProdutoDto>(vm);

        Result resultado = servicoProduto.Excluir(dto);

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<ListarProdutoDto> resultado = servicoProduto.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        EditarProdutoViewModel vm = mapper.Map<EditarProdutoViewModel>(resultado.Value);

        return View(vm);
    }
    [HttpPost]
    public ActionResult Editar(EditarProdutoViewModel vm)
    {
        if (!ModelState.IsValid)
            return View();

        EditarProdutoDto dto = mapper.Map<EditarProdutoDto>(vm);

        Result resultado = servicoProduto.Editar(dto);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
        }
        return RedirectToAction(nameof(Listar));
    }
}
