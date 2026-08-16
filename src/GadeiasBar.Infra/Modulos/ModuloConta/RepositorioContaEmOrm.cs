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
        .SingleOrDefault(c => c.Id == idSelecionado);
    }
    public override List<Conta> SelecionarTodos()
    {
        return registros.
        Include(c => c.Garcom)
       .Include(c => c.Mesa)
       .ToList();
    }
}
