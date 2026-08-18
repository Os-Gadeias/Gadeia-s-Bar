using GadeiasBar.Dominio.Modulos.ModuloProduto;

namespace GadeiasBar.Aplicacao.Modulos.ModuloPedido;

public record ListarPedidoDto(
    Guid Id,
    Produto Produto,
    int Quantidade
);

public record CadastrarPedidoDto(
    Produto Produto,
    int Quantidade
);

public record EditarPedidoDto(
    Guid Id,
    Produto Produto,
    int Quantidade
);

public record ExcluirPedidoDto(
    Guid Id,
    Produto Produto,
    int Quantidade
);
