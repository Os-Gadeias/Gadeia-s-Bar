using System.ComponentModel.DataAnnotations;
using GadeiasBar.Dominio.Modulos.ModuloProduto;

namespace GadeiasBar.WebApp.Modulos.ModuloPedido;

public record ListarPedidoViewModel(
    Guid Id,
    Produto Produto,
    int Quantidade
);

public record CadastrarPedidoViewModel(

    [Required(ErrorMessage = "O campo \"Produto\" deve ser preenchido")]
    Produto Produto,

    [Required(ErrorMessage = "O campo \"Quantidade\" deve ser preenchido")]
    [Range(0, int.MaxValue, ErrorMessage = "O campo \"Quantidade\" deve conter um valor maior que 0")]
    int Quantidade
);

public record EditarPedidoViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Produto\" deve ser preenchido")]
    Produto Produto,

    [Required(ErrorMessage = "O campo \"Quantidade\" deve ser preenchido")]
    [Range(1, int.MaxValue, ErrorMessage = "O campo \"Quantidade\" deve conter um valor maior que 0")]
    int Quantidade
);

public record ExcluirPedidoViewModel(
    Guid Id,
    Produto Produto,
    int Quantidade
);
