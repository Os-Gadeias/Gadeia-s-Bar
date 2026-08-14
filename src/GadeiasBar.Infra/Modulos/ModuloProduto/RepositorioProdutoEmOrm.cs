using GadeiasBar.Dominio.Modulos.ModuloProduto;
using GadeiasBar.Dominio.Modulos.ModuloProduto.cs;
using GadeiasBar.Infra.Compartilhado.Orm;

namespace GadeiasBar.Infra.Modulos.ModuloProduto;

public class RepositorioProdutoEmOrm(GadeiasBarDbContext dbContext) : RepositorioBaseEmOrm<Produto>(dbContext), IRepositorioProduto { }
