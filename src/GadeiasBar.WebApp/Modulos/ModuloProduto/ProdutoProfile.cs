using AutoMapper;
using GadeiasBar.Aplicacao.Modulos.ModuloProduto.cs;

namespace GadeiasBar.WebApp.Modulos.ModuloProduto;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<ListarProdutoDto, ListarProdutoViewModel>();
        CreateMap<CadastrarProdutoViewModel, CadastrarProdutoDto>();
        CreateMap<ListarProdutoDto, ExcluirProdutoViewModel>();
        CreateMap<ListarProdutoDto, EditarProdutoViewModel>();
        CreateMap<ExcluirProdutoViewModel, ExcluirProdutoDto>();
        CreateMap<EditarProdutoViewModel, EditarProdutoDto>();
    }
}
