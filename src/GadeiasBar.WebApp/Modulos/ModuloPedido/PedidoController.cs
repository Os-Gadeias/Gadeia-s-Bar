using AutoMapper;
using GadeiasBar.Aplicacao.Modulos.ModuloPedido;
using GadeiasBar.WebApp.Modulos.ModuloPedido;
using Microsoft.AspNetCore.Mvc;

public class PedidoController(IMapper mapper, ServicoPedido servicoPedido) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarPedidoDto> dtos = servicoPedido.SelecionarTodos();
        List<ListarPedidoViewModel> vm = mapper.Map<List<ListarPedidoViewModel>>(dtos);
        return View(vm);
    }
}
