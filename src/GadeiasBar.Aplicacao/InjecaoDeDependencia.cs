using GadeiasBar.Aplicacao.Modulos.ModuloMesa;
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
        // services.AddScoped<ServicoMesa>();
        services.AddScoped<ServicoMesa>();
    }
}
