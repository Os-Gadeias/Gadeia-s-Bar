namespace GadeiasBar.WebApp.Modulos.ModuloGarcom;

public record ListarGarcomViewModels(
    string Id,
    string Nome
);
public record CadastrarGarcomViewModels(
    string Nome
);
public record EditarGarcomViewModels(
    string Id,
    string Nome
);
public record ExcluirGarcomViewModels(
    string Id,
    string Nome
);