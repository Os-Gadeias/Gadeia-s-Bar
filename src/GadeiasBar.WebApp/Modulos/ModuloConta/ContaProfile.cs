using AutoMapper;
using GadeiasBar.Aplicacao.Modulos.ModuloConta;

namespace GadeiasBar.WebApp.Modulos.ModuloConta;

public class ContaProfile : Profile
{
    public ContaProfile()
    {
        CreateMap<ListarContaDto, ListarContaViewModel>();
        CreateMap<CadastrarContaViewModel, CadastrarContaDto>();
        CreateMap<ListarContaDto, ExcluirContaViewModel>();
        CreateMap<ExcluirContaViewModel, ExcluirContaDto>();
    }
}
