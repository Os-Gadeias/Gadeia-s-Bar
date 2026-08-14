using System.ComponentModel.DataAnnotations;
using GadeiasBar.Dominio.Modulos.ModuloProduto.cs;

namespace GadeiasBar.WebApp.Modulos.ModuloProduto;

public record ListarProdutoViewModel(
    Guid Id,
    string Nome,
    TipoProduto TipoProduto,
    decimal Valor
);
public record CadastrarProdutoViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" é obrigatório!")]
    [MinLength( 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 à 100 caracteres!")]
    string Nome,
    [Required(ErrorMessage = "O campo \"Tipo Produto\" é obrigatório!")]
    TipoProduto TipoProduto,
    [Required(ErrorMessage = "O campo \"Valor\" é obrigatório!")]
    decimal Valor
);
public record ExcluirProdutoViewModel(
    Guid Id,
    string Nome,
    TipoProduto TipoProduto,
    decimal Valor
);