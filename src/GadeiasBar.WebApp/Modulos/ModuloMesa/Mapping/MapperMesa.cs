using AutoMapper;

namespace GadeiasBar.WebApp.Modulos.ModuloMesa.Mapping;

public class MapperMesa : Profile
{
    public MapperMesa()
    {
        CreateMap<ListarMesaDto, ListarMesaViewModel>();
        CreateMap<CadastrarMesaViewModel, CadastrarMesaDto>();
        CreateMap<EditarMesaViewModel, EditarMesaDto>();
        CreateMap<ListarMesaDto, EditarMesaViewModel>();
        CreateMap<ListarMesaDto, ExcluirMesaViewModel>();
        CreateMap<ExcluirMesaDto, ExcluirMesaViewModel>();
        CreateMap<ExcluirMesaViewModel, ExcluirMesaDto>();
    }
}
