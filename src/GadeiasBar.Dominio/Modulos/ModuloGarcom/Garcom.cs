using GadeiasBar.Dominio.Compartilhado;
using GadeiasBar.Dominio.Compartilhado.Identity;

namespace GadeiasBar.Dominio.Modulos.ModuloGarcom;

public class Garcom : EntidadeBase<Garcom>, IEntidadeDoUsuario
{
    public string Nome { get; set; } = string.Empty;
    public Guid UserId { get; set; }

    public Garcom()
    {
    }
    public Garcom(string nome)
    {
        Nome = nome;
    }
    public override void Atualizar(Garcom entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (String.IsNullOrWhiteSpace(Nome))
            erros.Add("O campo \"Nome\" é obrigatório.");

        if (Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 2 à 100 caracteres.");

        return erros;
    }
}
