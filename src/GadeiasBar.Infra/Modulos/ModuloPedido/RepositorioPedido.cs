using GadeiasBar.Dominio.Modulos.ModuloMesa;
using GadeiasBar.Dominio.Modulos.ModuloPedido;
using GadeiasBar.Infra.Compartilhado.Orm;

namespace GadeiasBar.Infra.Modulos.ModuloPedido;

public class RepositorioPedidoEmOrm(GadeiasBarDbContext dbContext)
: RepositorioBaseEmOrm<Pedido>(dbContext), IRepositorioPedido;
