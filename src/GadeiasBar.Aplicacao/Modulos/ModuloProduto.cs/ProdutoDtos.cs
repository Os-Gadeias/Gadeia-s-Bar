using GadeiasBar.Dominio.Modulos.ModuloProduto.cs;

namespace GadeiasBar.Aplicacao.Modulos.ModuloProduto.cs;

public record ListarProdutoDto(
    Guid Id,
    string Nome,
    TipoProduto TipoProduto,
    decimal Valor
);
public record CadastrarProdutoDto(
    string Nome,
    TipoProduto TipoProduto,
    decimal Valor
);
public record ExcluirProdutoDto(
    Guid Id,
    string Nome,
    TipoProduto TipoProduto,
    decimal Valor
);