using AutoMapper;
using GadeiasBar.Aplicacao.Modulos.ModuloConta;
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
}
