using Microsoft.AspNetCore.Mvc;

namespace GadeiasBar.WebApp.Modulos.ModuloMesa;

public class MesaController : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        return View();
    }
}
