using GadeiasBar.Dominio.Modulos.ModuloConta;

namespace GadeiasBar.WebApp.Modulos.ModuloConta;

public record ListarContaViewModel(
    Guid Id,
    string NomeCliente,
    string Garcom,
    string Mesa,
    DateTime DataDeAbertura,
    DateTime? DataDeFechamento,
    StatusConta StatusConta
);
public record CadastrarContaViewModel(
    string NomeCliente,
    string Garcom,
    string Mesa
);
public record EditarContaViewModel(
    Guid Id,
    string NomeCliente,
    string Garcom,
    string Mesa
);
public record ExcluirContaViewModel(
    Guid Id,
    string NomeCliente,
    string Garcom,
    string Mesa,
    DateTime DataDeAbertura,
    DateTime? DataDeFechamento,
    StatusConta StatusConta
);