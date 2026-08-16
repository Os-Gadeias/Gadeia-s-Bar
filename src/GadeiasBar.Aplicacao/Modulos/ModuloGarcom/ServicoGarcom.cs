using GadeiasBar.Aplicacao.Compartilhado;
using GadeiasBar.Dominio.Modulos.ModuloGarcom;

namespace GadeiasBar.Aplicacao.Modulos.ModuloGarcom;

public class ServicoGarcom(IRepositorioGarcom repositorioGarcom) : ServicoBase<Garcom>
{
    public List<ListarGarcomDto> SelecionarTodos()
    {
        return repositorioGarcom.SelecionarTodos().Select(g =>
        new ListarGarcomDto(g.Id, g.Nome))
        .ToList();
    }
}
