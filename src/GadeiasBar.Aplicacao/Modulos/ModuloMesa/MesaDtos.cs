using GadeiasBar.Dominio.Modulos.ModuloMesa;

public record ListarMesaDto(
    Guid Id,
    int NumeroMesa,
    int QuantidadeLugares,
    StatusMesa StatusMesa
);

public record CadastrarMesaDto(
    int NumeroMesa,
    int QuantidadeLugares,
    StatusMesa StatusMesa
);

public record EditarMesaDto(
    Guid Id,
    int NumeroMesa,
    int QuantidadeLugares,
    StatusMesa StatusMesa
);

public record ExcluirMesaDto(
    Guid Id,
    int NumeroMesa,
    int QuantidadeLugares,
    StatusMesa StatusMesa
);
