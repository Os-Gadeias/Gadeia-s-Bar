using GadeiasBar.Aplicacao.Modulos.ModuloConta;
using GadeiasBar.Aplicacao.Modulos.ModuloGarcom;
using GadeiasBar.Aplicacao.Modulos.ModuloMesa;
using GadeiasBar.Aplicacao.Modulos.ModuloProduto.cs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GadeiasBar.Aplicacao;

public static class InjecaoDeDependencia
{
    public static void AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddScoped<ServicoProduto>();
        services.AddScoped<ServicoGarcom>();
        services.AddScoped<ServicoMesa>();
        services.AddScoped<ServicoConta>();
    }
}
