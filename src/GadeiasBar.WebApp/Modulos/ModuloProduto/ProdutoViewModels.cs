using GadeiasBar.Dominio.Modulos.ModuloProduto.cs;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace GadeiasBar.WebApp.Modulos.ModuloProduto;

public record ListarProdutoViewModel(
    Guid Id,
    string Nome,
    TipoProduto TipoProduto,
    decimal Valor
);
