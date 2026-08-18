using AutoMapper;
using FluentResults;
using GadeiasBar.Aplicacao.Modulos.ModuloPedido;
using GadeiasBar.WebApp.Compartilhado.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GadeiasBar.WebApp.Modulos.ModuloPedido;

public class PedidoController(IMapper mapper, ServicoPedido servicoPedido) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarPedidoDto> dtos = servicoPedido.SelecionarTodos();
        List<ListarPedidoViewModel> vm = mapper.Map<List<ListarPedidoViewModel>>(dtos);
        return View(vm);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarPedidoViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        CadastrarPedidoDto dto = mapper.Map<CadastrarPedidoDto>(vm);
        Result result = servicoPedido.Cadastrar(dto);

        if (result.IsFailed)
        {
            ModelState.AddModelError(result);
            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
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

        ExcluirMesaViewModel vm = mapper.Map<ExcluirMesaViewModel>(result.Value);

        return View(vm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirPedidoViewModel vm)
    {
        ExcluirPedidoDto dto = mapper.Map<ExcluirPedidoDto>(vm);

        Result result = servicoPedido.Excluir(dto);

        return RedirectToAction(nameof(Listar));
    }
}
