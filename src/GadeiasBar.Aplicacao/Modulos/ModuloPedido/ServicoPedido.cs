using FluentResults;
using GadeiasBar.Aplicacao.Compartilhado;
using GadeiasBar.Aplicacao.Modulos.ModuloPedido;
using GadeiasBar.Dominio.Modulos.ModuloConta;
using GadeiasBar.Dominio.Modulos.ModuloPedido;
using GadeiasBar.Dominio.Modulos.ModuloProduto;
using GadeiasBar.Dominio.Modulos.ModuloProduto.cs;

public class ServicoPedido(
    IRepositorioPedido repositorioPedido,
    IRepositorioProduto repositorioProduto,
    IRepositorioConta repositorioConta) : ServicoBase<Pedido>
{
    public Result Cadastrar(CadastrarPedidoDto dto)
    {
        Conta? conta = repositorioConta.SelecionarPorId(dto.ContaId);

        if (conta == null)
            return Falha(nameof(dto.ContaId), "Conta não encontrada");

        // Buscar o produto pelo ID
        Produto? produto = repositorioProduto.SelecionarPorId(dto.ProdutoId);

        // Validar se o produto existe
        if (produto == null)
            return Falha(string.Empty, "Produto não encontrado");

        // Criar o pedido com o Produto completo
        Pedido pedido = new Pedido(conta, produto, dto.Quantidade);

        Result result = ValidarEntidade(pedido);

        if (result.IsFailed)
            return result;

        repositorioPedido.Cadastrar(pedido);

        return Result.Ok();
    }

    public Result Excluir(ExcluirPedidoDto dto)
    {
        Pedido? pedido = repositorioPedido.SelecionarPorId(dto.Id);

        if (pedido == null)
            return Falha(string.Empty, "Pedido não encontrado");

        repositorioPedido.Excluir(dto.Id);

        return Result.Ok();
    }

    public Result Editar(EditarPedidoDto dto)
    {
        Produto? produto = repositorioProduto.SelecionarPorId(dto.ProdutoId);

        Pedido? pedido = repositorioPedido.SelecionarPorId(dto.Id);

        if (produto == null)
            return Falha(string.Empty, "Produto não encontrado");

        if (pedido == null)
            return Falha(string.Empty, "Pedido não encontrado");

        pedido.Atualizar(new Pedido(pedido.Conta, produto, dto.Quantidade));

        Result result = ValidarEntidade(pedido);

        if (result.IsFailed)
            return result;

        repositorioPedido.Editar(dto.Id, pedido);

        return Result.Ok();
    }

    public List<ListarPedidoDto> SelecionarTodos()
    {
        return repositorioPedido
        .SelecionarTodos()
        .Select(p => new ListarPedidoDto(
            p.Id,
            p.ContaId,
            p.Produto.Nome,
            p.Quantidade
        )).ToList();
    }

    public Result<ListarPedidoDto> SelecionarPorId(Guid Id)
    {
        Pedido? pedido = repositorioPedido.SelecionarPorId(Id);

        if (pedido == null)
            return Result.Fail("Pedido não encontrado");

        return Result.Ok(new ListarPedidoDto(
            pedido.Id,
            pedido.ContaId,
            pedido.Produto.Nome,
            pedido.Quantidade
        ));
    }
}
