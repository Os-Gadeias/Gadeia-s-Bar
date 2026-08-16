using AutoMapper;
using FluentResults;
using GadeiasBar.Aplicacao.Modulos.ModuloMesa;
using GadeiasBar.Dominio.Modulos.ModuloMesa;
using GadeiasBar.Infra.Compartilhado.Orm;
using GadeiasBar.WebApp.Compartilhado.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace GadeiasBar.WebApp.Modulos.ModuloMesa;

public class MesaController(IMapper mapper, ServicoMesa servicoMesa) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarMesaDto> dtos = servicoMesa.SelecionarTodos();
        List<ListarMesaViewModel> listarVm = mapper.Map<List<ListarMesaViewModel>>(dtos);
        return View(listarVm);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarMesaViewModel vm)
    {
        if (!ModelState.IsValid)
            return View(vm);

        CadastrarMesaDto dto = mapper.Map<CadastrarMesaDto>(vm);
        Result result = servicoMesa.Cadastrar(dto);

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
        Result<ListarMesaDto> result = servicoMesa.SelecionarPorId(Id);

        if (result.IsFailed)
        {
            TempData.AddErrorMessage(result);
            return RedirectToAction(nameof(Listar));
        }

        ExcluirMesaViewModel vm = mapper.Map<ExcluirMesaViewModel>(result.Value);

        return View(vm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirMesaViewModel vm)
    {
        ExcluirMesaDto dto = mapper.Map<ExcluirMesaDto>(vm);

        Result result = servicoMesa.Excluir(dto);

        return RedirectToAction(nameof(Listar));
    }
}
