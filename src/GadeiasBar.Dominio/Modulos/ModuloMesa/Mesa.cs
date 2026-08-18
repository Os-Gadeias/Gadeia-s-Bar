using GadeiasBar.Dominio.Compartilhado;
using GadeiasBar.Dominio.Compartilhado.Identity;

namespace GadeiasBar.Dominio.Modulos.ModuloMesa;

public class Mesa : EntidadeBase<Mesa>, IEntidadeDoUsuario
{
    public int NumeroMesa { get; set; } = 0;
    public int QuantidadeLugares { get; set; } = 0;
    public StatusMesa statusMesa { get; set; }
    public Guid UserId { get; set; }

    public Mesa() { }

    public Mesa(int numeroMesa, int quantidadeLugares, StatusMesa statusMesa)
    {
        NumeroMesa = numeroMesa;
        QuantidadeLugares = quantidadeLugares;
        this.statusMesa = statusMesa;
    }

    public override void Atualizar(Mesa entidadeAtualizada)
    {
        NumeroMesa = entidadeAtualizada.NumeroMesa;
        QuantidadeLugares = entidadeAtualizada.QuantidadeLugares;
        statusMesa = entidadeAtualizada.statusMesa;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (NumeroMesa == 0)
            erros.Add("O campo \"Numero Da Mesa\" deve ser preenchido");

        else if (NumeroMesa < 0)
            erros.Add("O campo \"Numero Da Mesa\" não pode ser um numero negativo");

        if (QuantidadeLugares == 0)
            erros.Add("O campo \"Quantidade De Lugares\" deve ser preenchido");

        else if (QuantidadeLugares < 0)
            erros.Add("O campo \"Quantidade De Lugares\" não pode ser um numero negativo");

        else if (QuantidadeLugares > 10)
            erros.Add("O campo \"Quantidade De Lugares\" não pode ultrapassar 10 lugares");

        if (!Enum.IsDefined(typeof(StatusMesa), statusMesa))
            erros.Add("O campo \"Status Da Mesa\" é inválido");

        return erros;
    }

    public void OcuparMesa(bool OcuparAMesa)
    {
        if (OcuparAMesa)
            statusMesa = StatusMesa.Ocupada;

        else
            statusMesa = StatusMesa.Livre;
    }
}

