using AutoMapper;
using FluentResults;
using GadeiasBar.Aplicacao.Modulos.ModuloConta;
using GadeiasBar.WebApp.Compartilhado.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GadeiasBar.WebApp.Modulos.ModuloConta;

public class ContaController(
    IMapper mapper,
    ServicoConta servicoConta
    ) : Controller
{
    public ActionResult Listar()
    {
        List<ListarContaDto> dtos = servicoConta.SelecionarTodos();
        List<ListarContaViewModel> vms = mapper.Map<List<ListarContaViewModel>>(dtos);
        return View(vms);
    }
    [HttpGet]
    public ActionResult Cadastrar()
    {
        ViewBag.Mesas = servicoConta.CarregarMesas();
        ViewBag.Garcons = servicoConta.CarregarGarcons();

        return View();
    }
    [HttpPost]
    public ActionResult Cadastrar(CadastrarContaViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Mesas = servicoConta.CarregarMesas();
            ViewBag.Garcons = servicoConta.CarregarGarcons();
            return View(vm);
        }

        CadastrarContaDto dto = mapper.Map<CadastrarContaDto>(vm);

        Result resultado = servicoConta.Cadastrar(dto);

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<ListarContaDto> resultado = servicoConta.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        ExcluirContaViewModel vm = mapper.Map<ExcluirContaViewModel>(resultado.Value);

        return View(vm);
    }
    [HttpPost]
    public ActionResult Excluir(ExcluirContaViewModel vm)
    {
        ExcluirContaDto dto = mapper.Map<ExcluirContaDto>(vm);

        Result resultado = servicoConta.Excluir(dto);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<ListarContaDto> resultado = servicoConta.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        EditarContaViewModel vm = mapper.Map<EditarContaViewModel>(resultado.Value);
        ViewBag.Mesas = servicoConta.CarregarMesas();
        ViewBag.Garcons = servicoConta.CarregarGarcons();

        return View(vm);
    }
    [HttpPost]
    public ActionResult Editar(EditarContaViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Mesas = servicoConta.CarregarMesas();
            ViewBag.Garcons = servicoConta.CarregarGarcons();
            return View(vm);
        }

        EditarContaDto dto = mapper.Map<EditarContaDto>(vm);

        Result resultado = servicoConta.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            ViewBag.Mesas = servicoConta.CarregarMesas();
            ViewBag.Garcons = servicoConta.CarregarGarcons();
            return View(vm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Visualizar(Guid id)
    {
        Result<ListarContaDto> resultado = servicoConta.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        ListarContaViewModel vm = mapper.Map<ListarContaViewModel>(resultado.Value);

        return View(vm);
    }
}
