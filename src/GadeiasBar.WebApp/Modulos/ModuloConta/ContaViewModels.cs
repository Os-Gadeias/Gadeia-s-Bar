using System.ComponentModel.DataAnnotations;
using GadeiasBar.Dominio.Modulos.ModuloConta;

namespace GadeiasBar.WebApp.Modulos.ModuloConta;

public record ListarContaViewModel(
    Guid Id,
    string NomeCliente,
    Guid IdGarcom,
    string Garcom,
    Guid IdMesa,
    int Mesa,
    string DataDeAbertura,
    string? DataDeFechamento,
    StatusConta StatusConta,
    decimal ValorFinal
);
public record CadastrarContaViewModel(
    [Required]
    string NomeCliente,
    [Required]
    string Garcom,
    [Required]
    string Mesa
);
public record EditarContaViewModel(
    Guid Id,
    string NomeCliente,
    Guid IdGarcom,
    Guid IdMesa
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