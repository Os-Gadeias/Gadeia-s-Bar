using GadeiasBar.Dominio.Modulos.ModuloGarcom;
using GadeiasBar.Dominio.Modulos.ModuloMesa;
using GadeiasBar.Dominio.Modulos.ModuloProduto.cs;
using GadeiasBar.Infra.Compartilhado.Logging;
using GadeiasBar.Infra.Compartilhado.Orm;
using GadeiasBar.Infra.Modulos.ModuloGarcom;
using GadeiasBar.Infra.Modulos.ModuloMesa;
using GadeiasBar.Infra.Modulos.ModuloProduto;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace GadeiasBar.Infra;

public static class InjecaoDeDependencia
{
    public static void AddInfraRepositories(
        this IServiceCollection services,
        IConfiguration configuration,
        ILoggingBuilder logging,
        IHostEnvironment environment
    )
    {
        Serilog.ILogger logger = SerilogFactory.Create(configuration, environment);

        logging.ClearProviders();
        services.AddSerilog(logger, dispose: true);

        services.AddDbContext<GadeiasBarDbContext>(options =>
        {
            string? connectionString = configuration.GetConnectionString("SqlServerEF");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    $"A connection string \"SqlServerEF\" não foi encontrada."
                );
            }

            options.UseSqlServer(connectionString, opt =>
            {
                opt.EnableRetryOnFailure(3);
            });
        });

        services.AddIdentityCore<IdentityUser<Guid>>(options =>
        {
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;
        })
        .AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<GadeiasBarDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();

        services.AddScoped<IRepositorioMesa, RepositorioMesaEmOrm>();
        services.AddScoped<IRepositorioProduto, RepositorioProdutoEmOrm>();
        services.AddScoped<IRepositorioGarcom, RepositorioGarcomEmOrm>();
    }
}
