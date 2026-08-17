using GadeiasBar.Dominio.Modulos.ModuloConta;

namespace GadeiasBar.Aplicacao.Modulos.ModuloConta;

public record ListarContaDto(
    Guid Id,
    string NomeCliente,
    string Garcom,
    int Mesa,
    DateTime DataDeAbertura,
    DateTime? DataDeFechamento,
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
    string Garcom,
    int Mesa
);
public record ExcluirContaDto(
    Guid Id,
    string NomeCliente,
    string Garcom,
    int Mesa,
    DateTime DataDeAbertura,
    DateTime? DataDeFechamento,
    StatusConta StatusConta,
    decimal ValorFinal
);