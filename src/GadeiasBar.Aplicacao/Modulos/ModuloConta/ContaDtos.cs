using GadeiasBar.Dominio.Modulos.ModuloConta;

namespace GadeiasBar.Aplicacao.Modulos.ModuloConta;

public record ListarContaDto(
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
public record CadastrarContaDto(
    string NomeCliente,
    string Garcom,
    string Mesa
);
public record EditarContaDto(
    Guid Id,
    string NomeCliente,
    Guid IdGarcom,
    Guid IdMesa
);
public record ExcluirContaDto(
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