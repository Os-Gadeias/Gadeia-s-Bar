using AutoMapper;
using GadeiasBar.Aplicacao.Modulos.ModuloProduto.cs;

namespace GadeiasBar.WebApp.Modulos.ModuloProduto;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<ListarProdutoDto, ListarProdutoViewModel>();
    }
}
