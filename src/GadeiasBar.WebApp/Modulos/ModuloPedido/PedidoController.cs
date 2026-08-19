using AutoMapper;
using FluentResults;
using GadeiasBar.Aplicacao.Modulos.ModuloPedido;
using GadeiasBar.Aplicacao.Modulos.ModuloProduto;
using GadeiasBar.Aplicacao.Modulos.ModuloProduto.cs;
using GadeiasBar.Dominio.Modulos.ModuloProduto;
using GadeiasBar.WebApp.Compartilhado.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GadeiasBar.WebApp.Modulos.ModuloPedido;

public class PedidoController(
    IMapper mapper,
    ServicoPedido servicoPedido,
    ServicoProduto servicoProduto) : Controller
{
    [HttpGet]
    public ActionResult Listar(Guid contaId)
    {
        ViewBag.ContaId = contaId;
        List<ListarPedidoDto> dtos = servicoPedido.SelecionarTodos()
            .Where(p => p.ContaId == contaId)
            .ToList();
        List<ListarPedidoViewModel> vm = mapper.Map<List<ListarPedidoViewModel>>(dtos);
        return View(vm);
    }

    [HttpGet]
    public ActionResult Cadastrar(Guid contaId)
    {
        ViewBag.ContaId = contaId;
        List<ListarProdutoDto> produtos = servicoProduto.SelecionarTodos();
        ViewBag.Produtos = produtos;
        return View(new CadastrarPedidoViewModel(contaId, new Produto(), 0));
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarPedidoViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.ContaId = vm.ContaId;
            ViewBag.Produtos = servicoProduto.SelecionarTodos();
            return View(vm);
        }

        CadastrarPedidoDto dto = mapper.Map<CadastrarPedidoDto>(vm);
        Result result = servicoPedido.Cadastrar(dto);

        if (result.IsFailed)
        {
            ModelState.AddModelError(result);
            ViewBag.ContaId = vm.ContaId;
            ViewBag.Produtos = servicoProduto.SelecionarTodos();
            return View(vm);
        }

        return RedirectToAction(nameof(Listar), new { contaId = vm.ContaId });
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<ListarPedidoDto> result = servicoPedido.SelecionarPorId(id);

        if (result.IsFailed)
        {
            TempData.AddErrorMessage(result);
            return RedirectToAction(nameof(Listar));
        }

        EditarPedidoViewModel vm = mapper.Map<EditarPedidoViewModel>(result.Value);

        List<ListarProdutoDto> produtos = servicoProduto.SelecionarTodos();
        ViewBag.Produtos = produtos;

        return View(vm);
    }

    [HttpPost]
    public ActionResult Editar(EditarPedidoViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            List<ListarProdutoDto> produtos = servicoProduto.SelecionarTodos();
            ViewBag.Produtos = produtos;
            return View(vm);
        }

        EditarPedidoDto dto = mapper.Map<EditarPedidoDto>(vm);
        Result result = servicoPedido.Editar(dto);

        if (result.IsFailed)
        {
            ModelState.AddModelError(result);
            List<ListarProdutoDto> produtos = servicoProduto.SelecionarTodos();
            ViewBag.Produtos = produtos;
            return View(vm);
        }

        return RedirectToAction(nameof(Listar), new { contaId = vm.ContaId });
    }

    [HttpGet]
    public ActionResult Excluir(Guid Id)
    {
        Result<ListarPedidoDto> result = servicoPedido.SelecionarPorId(Id);

        if (result.IsFailed)
        {
            TempData.AddErrorMessage(result);
            return RedirectToAction(nameof(Listar));
        }

        ExcluirPedidoViewModel vm = mapper.Map<ExcluirPedidoViewModel>(result.Value);

        return View(vm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirPedidoViewModel vm)
    {
        ExcluirPedidoDto dto = mapper.Map<ExcluirPedidoDto>(vm);

        Result result = servicoPedido.Excluir(dto);

        return RedirectToAction(nameof(Listar), new { contaId = vm.ContaId });
    }
}
