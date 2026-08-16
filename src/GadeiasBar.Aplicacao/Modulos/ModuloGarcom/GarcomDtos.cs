namespace GadeiasBar.Aplicacao.Modulos.ModuloGarcom;

public record ListarGarcomDto(
    string Id,
    string Nome
);
public record CadastrarGarcomDto(
    string Nome
);
public record EditarGarcomDto(
    string Id,
    string Nome
);
public record ExcluirGarcomDto(
    string Id,
    string Nome
);