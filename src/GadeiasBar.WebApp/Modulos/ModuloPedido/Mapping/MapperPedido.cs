using AutoMapper;
using GadeiasBar.Aplicacao.Modulos.ModuloPedido;
using GadeiasBar.WebApp.Modulos.ModuloPedido;

namespace GadeiasBar.WebApp.Modulos.ModuloPedido.Mapping;

public class MapperPedido : Profile
{
    public MapperPedido()
    {
        CreateMap<ListarPedidoDto, ListarPedidoViewModel>();
        CreateMap<CadastrarPedidoViewModel, CadastrarPedidoDto>();
        CreateMap<EditarPedidoViewModel, EditarPedidoDto>();
        CreateMap<ListarPedidoDto, EditarPedidoViewModel>();
        CreateMap<ListarPedidoDto, ExcluirPedidoViewModel>();
        CreateMap<ExcluirPedidoDto, ExcluirPedidoViewModel>();
        CreateMap<ExcluirPedidoViewModel, ExcluirPedidoDto>();
    }
}
