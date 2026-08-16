using AutoMapper;
using GadeiasBar.Aplicacao.Modulos.ModuloGarcom;
using Microsoft.AspNetCore.Mvc;

namespace GadeiasBar.WebApp.Modulos.ModuloGarcom;

public class GarcomController(IMapper mapper, ServicoGarcom servicoGarcom) : Controller
{
    public ActionResult Listar()
    {
        List<ListarGarcomDto> dtos = servicoGarcom.SelecionarTodos();
        List<ListarGarcomViewModels> vms = mapper.Map<List<ListarGarcomViewModels>>(dtos);
        return View(vms);
    }
}
