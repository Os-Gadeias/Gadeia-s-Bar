using GadeiasBar.Aplicacao.Compartilhado;
using GadeiasBar.Dominio.Modulos.ModuloConta;
using GadeiasBar.Dominio.Modulos.ModuloGarcom;

namespace GadeiasBar.Aplicacao.Modulos.ModuloConta;

public class ServicoConta(
    IRepositorioConta repositorioConta,
    IRepositorioMesa repositorioMesa,
    IRepositorioGarcom repositorioGarcom) : ServicoBase<Conta>
{
    public List<ListarContaDto> SelecionarTodos()
    {
        return repositorioConta.SelecionarTodos().Select(c => new ListarContaDto(c.Id, c.NomeCliente, c.Garcom.Nome, c.Mesa.NumeroMesa
        , c.DataDeAbertura, c.DataDeFechamento, c.StatusConta, c.ValorFinal
        )).ToList();
    }
}
