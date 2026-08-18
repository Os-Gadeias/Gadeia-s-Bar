using System.ComponentModel.DataAnnotations;
using GadeiasBar.Dominio.Modulos.ModuloConta;

namespace GadeiasBar.WebApp.Modulos.ModuloConta;

public record ListarContaViewModel(
    Guid Id,
    string NomeCliente,
    string Garcom,
    Guid IdMesa,
    int Mesa,
    string DataDeAbertura,
    string? DataDeFechamento,
    StatusConta StatusConta,
    decimal ValorFinal
);
public record CadastrarContaViewModel(
    [Required(ErrorMessage = "O campo \"Nome Cliente\" é obrigatório!")]
    string NomeCliente,
    [Required(ErrorMessage = "O campo \"Garcom\" é obrigatório!")]
    string Garcom,
    [Required(ErrorMessage = "O campo \"Mesa\" é obrigatório!")]
    string Mesa
);
public record EditarContaViewModel(
    Guid Id,
    string NomeCliente,
    string Garcom,
    Guid IdMesa,
    string Mesa
);
public record ExcluirContaViewModel(
    Guid Id,
    string NomeCliente,
    string Garcom,
    Guid IdMesa,
    int Mesa,
    string DataDeAbertura,
    string? DataDeFechamento,
    StatusConta StatusConta,
    decimal ValorFinal
);