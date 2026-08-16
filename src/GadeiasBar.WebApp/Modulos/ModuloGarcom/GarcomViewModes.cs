namespace GadeiasBar.WebApp.Modulos.ModuloGarcom;

public record ListarGarcomViewModels(
    Guid Id,
    string Nome
);
public record CadastrarGarcomViewModels(
    Guid Nome
);
public record EditarGarcomViewModels(
    Guid Id,
    string Nome
);
public record ExcluirGarcomViewModels(
    Guid Id,
    string Nome
);