using Microsoft.AspNetCore.Mvc;

namespace GadeiasBar.WebApp.Compartilhado;

public class HomeController : Controller
{
    [HttpGet]
    public ActionResult Index()
    {
        return View();
    }
}
