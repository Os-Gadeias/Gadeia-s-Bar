using System.ComponentModel.DataAnnotations;
using GadeiasBar.Dominio.Modulos.ModuloMesa;

public record ListarMesaViewModel(
    Guid Id,
    int NumeroMesa,
    int QuantidadeLugares,
    StatusMesa StatusMesa
);

public record CadastrarMesaViewModel(
    Guid Id,

    [Required(ErrorMessage ="O campo \"Numero Da Mesa\" deve ser preenchido")]
    [Range(0, int.MaxValue, ErrorMessage = "O numero da Mesa deve ser maior que 0")]
    int NumeroMesa,

    [Required(ErrorMessage = "O campo \"Quantidade De Lugares\" deve ser preenchido")]
    [Range(0, maximum:10, ErrorMessage = "O campo \"Quantidade De Lugares\" deve conter entre 1 a 10 lugares")]
    int QuantidadeLugares,

    [EnumDataType(typeof(StatusMesa), ErrorMessage = "O campo \"Status Da Mesa\" deve conter uma resposta valida")]
    StatusMesa StatusMesa
);

public record EditarMesaViewModel(
    Guid Id,

    [Required(ErrorMessage ="O campo \"Numero Da Mesa\" deve ser preenchido")]
    [Range(0, int.MaxValue, ErrorMessage = "O numero da Mesa deve ser maior que 0")]
    int NumeroMesa,

    [Required(ErrorMessage = "O campo \"Quantidade De Lugares\" deve ser preenchido")]
    [Range(0, maximum:10, ErrorMessage = "O campo \"Quantidade De Lugares\" deve conter entre 1 a 10 lugares")]
    int QuantidadeLugares,

    [EnumDataType(typeof(StatusMesa), ErrorMessage = "O campo \"Status Da Mesa\" deve conter uma resposta valida")]
    StatusMesa StatusMesa
);
public record ExcluirMesaViewModel(
    Guid Id,
    int NumeroMesa,
    int QuantidadeLugares,
    StatusMesa StatusMesa
);
