using GadeiasBar.Dominio.Compartilhado;
using GadeiasBar.Dominio.Compartilhado.Identity;
using GadeiasBar.Dominio.Modulos.ModuloProduto;

namespace GadeiasBar.Dominio.Modulos.ModuloPedido;

public class Pedido : EntidadeBase<Pedido>, IEntidadeDoUsuario
{
    public Produto Produto { get; set; } = null!;
    public int Quantidade { get; set; } = 0;
    public Guid UserId { get; set; }

    public Pedido() { }

    public Pedido(Produto produto, int quantidade)
    {
        Produto = produto;
        Quantidade = quantidade;
    }

    public override void Atualizar(Pedido entidadeAtualizada)
    {
        Produto = entidadeAtualizada.Produto;
        Quantidade = entidadeAtualizada.Quantidade;
    }

    public override List<string> Validar()
    {
        List<string> erros = new List<string>();

        if (Quantidade == 0)
            erros.Add("O campo \"Quantidae\" n pode sem 0");

        else if (Quantidade < 0)
            erros.Add("O campo \"Quantidade\" n pode ser um numero negativo");

        if (Produto == null)
            erros.Add("O campo \"Produto\" deve ser preenchido");

        return erros;
    }
}
