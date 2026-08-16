using System.ComponentModel.DataAnnotations;

namespace GadeiasBar.WebApp.Modulos.ModuloGarcom;

public record ListarGarcomViewModels(
    Guid Id,
    string Nome
);
public record CadastrarGarcomViewModels(
    [Required (ErrorMessage = "O campo \"Nome\" é obrigatório")]
    string Nome
);
public record EditarGarcomViewModels(
    Guid Id,
    [Required (ErrorMessage = "O campo \"Nome\" é obrigatório")]
    string Nome
);
public record ExcluirGarcomViewModels(
    Guid Id,
    string Nome
);