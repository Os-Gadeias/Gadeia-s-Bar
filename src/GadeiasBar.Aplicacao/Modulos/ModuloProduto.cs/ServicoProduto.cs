using GadeiasBar.Aplicacao.Compartilhado;
using GadeiasBar.Dominio.Modulos.ModuloProduto.cs;

namespace GadeiasBar.Aplicacao.Modulos.ModuloProduto.cs;

public class ServicoProduto(IRepositorioProduto repositorioProduto) : ServicoBase<Produto>
{
    public List<ListarProdutoDto> SelecionarTodos()
    {
        return repositorioProduto.SelecionarTodos().Select(p =>
        new ListarProdutoDto(
            p.Id, p.Nome,
            p.TipoProduto, p.Valor
            )).ToList();
    }
}
