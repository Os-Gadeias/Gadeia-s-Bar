using GadeiasBar.Dominio.Modulos.ModuloMesa;
using GadeiasBar.Infra.Compartilhado.Orm;

namespace GadeiasBar.Infra.Modulos.ModuloMesa;

public class RepositorioMesaEmOrm(GadeiasBarDbContext dbContext)
: RepositorioBaseEmOrm<Mesa>(dbContext), IRepositorioMesa;
