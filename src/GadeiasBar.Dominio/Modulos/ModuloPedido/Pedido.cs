using GadeiasBar.Dominio.Compartilhado;
using GadeiasBar.Dominio.Modulos.ModuloProduto;

namespace GadeiasBar.Dominio.Modulos.ModuloPedido;

public class Pedido : EntidadeBase<Pedido>
{
    public Produto Produto {get; set;} = null!;
    public int Quantidade { get; set;}

    public override void Atualizar(Pedido entidadeAtualizada)
    {
        throw new NotImplementedException();
    }

    public override List<string> Validar()
    {
        throw new NotImplementedException();
    }
}
