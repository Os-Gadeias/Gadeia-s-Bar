using GadeiasBar.Dominio.Modulos.ModuloPedido;
using GadeiasBar.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace GadeiasBar.Infra.Modulos.ModuloPedido;

public class RepositorioPedidoEmOrm(GadeiasBarDbContext dbContext)
: RepositorioBaseEmOrm<Pedido>(dbContext), IRepositorioPedido
{
    public override Pedido? SelecionarPorId(Guid idSelecionado)
    {
        return registros
            .Include(p => p.Produto)
            .Include(p => p.Conta)
            .SingleOrDefault(p => p.Id == idSelecionado);
    }

    public override List<Pedido> SelecionarTodos()
    {
        return registros
            .Include(p => p.Produto)
            .Include(p => p.Conta)
            .ToList();
    }
}
