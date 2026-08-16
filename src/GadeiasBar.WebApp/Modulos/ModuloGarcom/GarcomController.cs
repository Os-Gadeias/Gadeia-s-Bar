using AutoMapper;
using FluentResults;
using GadeiasBar.Aplicacao.Modulos.ModuloGarcom;
using GadeiasBar.WebApp.Compartilhado.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GadeiasBar.WebApp.Modulos.ModuloGarcom;

public class GarcomController(IMapper mapper, ServicoGarcom servicoGarcom) : Controller
{
    public ActionResult Listar()
    {
        List<ListarGarcomDto> dtos = servicoGarcom.SelecionarTodos();
        List<ListarGarcomViewModels> vms = mapper.Map<List<ListarGarcomViewModels>>(dtos);
        return View(vms);
    }
    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }
    [HttpPost]
    public ActionResult Cadastrar(CadastrarGarcomViewModels vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        CadastrarGarcomDto dto = mapper.Map<CadastrarGarcomDto>(vm);

        Result resultado = servicoGarcom.Cadastrar(dto);

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
        Result<ListarGarcomDto> resultado = servicoGarcom.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        ExcluirGarcomViewModels vm = mapper.Map<ExcluirGarcomViewModels>(resultado.Value);

        return View(vm);
    }
    [HttpPost]
    public ActionResult Excluir(ExcluirGarcomViewModels vm)
    {
        ExcluirGarcomDto dto = mapper.Map<ExcluirGarcomDto>(vm);

        Result resultado = servicoGarcom.Excluir(dto);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }
    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<ListarGarcomDto> resultado = servicoGarcom.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);
            return RedirectToAction(nameof(Listar));
        }

        EditarGarcomViewModels vm = mapper.Map<EditarGarcomViewModels>(resultado.Value);

        return View(vm);
    }
    [HttpPost]
    public ActionResult Editar(EditarGarcomViewModels vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        EditarGarcomDto dto = mapper.Map<EditarGarcomDto>(vm);

        Result resultado = servicoGarcom.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);
            return View(vm);
        }
        return RedirectToAction(nameof(Listar));
    }
}