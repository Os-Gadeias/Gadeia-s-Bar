using AutoMapper;
using GadeiasBar.Aplicacao.Modulos.ModuloPedido;
using GadeiasBar.WebApp.Modulos.ModuloPedido;

namespace GadeiasBar.WebApp.Modulos.ModuloPedido.Mapping;

public class MapperPedido : Profile
{
    public MapperPedido()
    {
        CreateMap<ListarPedidoDto, ListarPedidoViewModel>();

        CreateMap<CadastrarPedidoViewModel, CadastrarPedidoDto>()
            .ForMember(dto => dto.ProdutoId, opt => opt.MapFrom(vm => vm.Produto.Id));

        CreateMap<EditarPedidoViewModel, EditarPedidoDto>()
            .ForMember(dto => dto.ProdutoId, opt => opt.MapFrom(vm => vm.Produto.Id));

        CreateMap<ListarPedidoDto, EditarPedidoViewModel>();
        CreateMap<ListarPedidoDto, ExcluirPedidoViewModel>();
        CreateMap<ExcluirPedidoViewModel, ExcluirPedidoDto>();
    }
}
