using GadeiasBar.Dominio.Compartilhado;

namespace GadeiasBar.Dominio.Modulos.ModuloProduto.cs;

public class Produto : EntidadeBase<Produto>
{
    public string Nome { get; set; } = string.Empty;
    public TipoProduto TipoProduto { get; set; }
    public decimal Valor { get; set; }
    public Guid UserId { get; set; }
    public Produto()
    {
    }
    public Produto(string nome, TipoProduto tipoProduto, decimal valor)
    {
        Nome = nome;
        TipoProduto = tipoProduto;
        Valor = valor;
    }

    public override void Atualizar(Produto entidadeAtualizada)
    {
        throw new NotImplementedException();
    }

    public override List<string> Validar()
    {
        throw new NotImplementedException();
    }
}
