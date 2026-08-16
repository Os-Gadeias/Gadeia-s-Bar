using AutoMapper;
using FluentResults;
using GadeiasBar.Aplicacao.Modulos.ModuloMesa;
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
}
