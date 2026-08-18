using GadeiasBar.Dominio.Modulos.ModuloProduto;

namespace GadeiasBar.Aplicacao.Modulos.ModuloPedido;

public record ListarPedidoDto(
    Guid Id,
    string ProdutoNome,
    int Quantidade
);

public record CadastrarPedidoDto(
    Guid ProdutoId,
    int Quantidade
);

public record EditarPedidoDto(
    Guid Id,
    Guid ProdutoId,
    int Quantidade
);

public record ExcluirPedidoDto(
    Guid Id,
    Guid ProdutoId,
    int Quantidade
);
