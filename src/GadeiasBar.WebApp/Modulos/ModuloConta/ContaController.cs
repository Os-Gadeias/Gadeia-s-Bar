using AutoMapper;
using FluentResults;
using GadeiasBar.Aplicacao.Modulos.ModuloConta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
}
