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
        

        return View();
    }
}
