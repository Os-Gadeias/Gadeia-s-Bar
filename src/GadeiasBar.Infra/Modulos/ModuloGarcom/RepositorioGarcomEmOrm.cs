using GadeiasBar.Dominio.Compartilhado;
using GadeiasBar.Dominio.Modulos.ModuloGarcom;
using GadeiasBar.Dominio.Modulos.ModuloProduto;
using GadeiasBar.Infra.Compartilhado.Orm;

namespace GadeiasBar.Infra.Modulos.ModuloGarcom;

public class RepositorioGarcomEmOrm(GadeiasBarDbContext dbContext) :
    RepositorioBaseEmOrm<Garcom>(dbContext), IRepositorioGarcom
{ }
