using GadeiasBar.Dominio.Compartilhado;
using GadeiasBar.Dominio.Compartilhado.Identity;
using GadeiasBar.Dominio.Modulos.ModuloGarcom;
using GadeiasBar.Dominio.Modulos.ModuloMesa;
using GadeiasBar.Dominio.Modulos.ModuloPedido;

namespace GadeiasBar.Dominio.Modulos.ModuloConta;

public class Conta : EntidadeBase<Conta>, IEntidadeDoUsuario
{
    public string NomeCliente { get; set; } = string.Empty;
    public Garcom Garcom { get; set; } = null!;
    public Mesa Mesa { get; set; } = null!;
    public DateTime DataDeAbertura { get; set; } = DateTime.Today;
    public DateTime? DataDeFechamento { get; set; } = null;
    public StatusConta StatusConta { get; set; } = StatusConta.Aberta;
    public List<Pedido> Pedidos { get; set; } = [];
    public Guid UserId { get; set; }
    public decimal ValorFinal
    {
        get
        {
            decimal valorTotal = 0;

            foreach (Pedido p in Pedidos)
                valorTotal += p.Produto.Valor * p.Quantidade;

            return valorTotal;
        }
    }

    public override void Atualizar(Conta entidadeAtualizada)
    {
        NomeCliente = entidadeAtualizada.NomeCliente;
        Garcom = entidadeAtualizada.Garcom;
        Mesa = entidadeAtualizada.Mesa;
        StatusConta = entidadeAtualizada.StatusConta;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(NomeCliente))
            erros.Add("O nome do cliente é obrigatório.");
        else if (NomeCliente.Length < 2 || NomeCliente.Length > 100)
            erros.Add("O Nome do cliente deve conter entre 2 à 100 caracteres");

        if (Garcom is null)
            erros.Add("O garçom é obrigatório.");

        if (Mesa is null)
            erros.Add("A mesa é obrigatória.");

        return erros;
    }
}
