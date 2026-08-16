using GadeiasBar.Dominio.Modulos.ModuloConta;
using GadeiasBar.Infra.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

namespace GadeiasBar.Infra.Modulos.ModuloConta;

public class RepositorioContaEmOrm(GadeiasBarDbContext dbContext) : RepositorioBaseEmOrm<Conta>(dbContext), IRepositorioConta
{
    public override Conta? SelecionarPorId(Guid idSelecionado)
    {
        return registros
            .Include(c => c.Garcom)
            .Include(c => c.Mesa)
            .Include(c => c.Pedidos)
                .ThenInclude(p => p.Produto)
        .SingleOrDefault(c => c.Id == idSelecionado);
    }
    public override List<Conta> SelecionarTodos()
    {
        return registros.
            Include(c => c.Garcom)
            .Include(c => c.Mesa)
            .Include(c => c.Pedidos)
                .ThenInclude(p => p.Produto)
       .ToList();
    }
}
