using GadeiasBar.Dominio.Compartilhado;
using GadeiasBar.Dominio.Compartilhado.Identity;

namespace GadeiasBar.Dominio.Modulos.ModuloProduto.cs;

public class Produto : EntidadeBase<Produto>, IEntidadeDoUsuario
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
        Nome = entidadeAtualizada.Nome;
        TipoProduto = entidadeAtualizada.TipoProduto;
        Valor = entidadeAtualizada.Valor;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (String.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" é obrigatório.");

        if (Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 2 à 100 caracteres.");

        if (!Enum.IsDefined(TipoProduto))
            erros.Add("O campo \"Tipo produto\" é obrigatório.");

        if (Valor <= 0)
            erros.Add("O campo \"Valor\" deve conter um valor positivo.");

        return erros;
    }
}
