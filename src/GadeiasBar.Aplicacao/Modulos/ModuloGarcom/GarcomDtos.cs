namespace GadeiasBar.Aplicacao.Modulos.ModuloGarcom;

public record ListarGarcomDto(
    Guid Id,
    string Nome
);
public record CadastrarGarcomDto(
    string Nome
);
public record EditarGarcomDto(
    Guid Id,
    string Nome
);
public record ExcluirGarcomDto(
    Guid Id,
    string Nome
);