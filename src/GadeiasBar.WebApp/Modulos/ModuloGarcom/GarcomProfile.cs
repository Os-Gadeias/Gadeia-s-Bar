using AutoMapper;
using GadeiasBar.Aplicacao.Modulos.ModuloGarcom;

namespace GadeiasBar.WebApp.Modulos.ModuloGarcom;

public class GarcomProfile : Profile
{
    public GarcomProfile()
    {
        CreateMap<ListarGarcomDto, ListarGarcomViewModels>();
        CreateMap<CadastrarGarcomViewModels, CadastrarGarcomDto>();
        CreateMap<ListarGarcomDto, ExcluirGarcomViewModels>();
        CreateMap<ExcluirGarcomViewModels, ExcluirGarcomDto>();
    }
}
