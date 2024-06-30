using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sutom.Application.Implementations;
using Sutom.Infrastructure.DataSources;
using Sutom.Absrtractions;


namespace Sutom.DenpendencyInjection
{
    public static class ServiceConfiguration
    {

        public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register data source strategies
            services.AddSingleton<IWordRemoteDataSource, WordRemoteDataSource>();
            services.AddSingleton<IWordLocalDataSource, WordLocalDataSource>();
            services.AddSingleton<IGameDataSource, GameDataSource>();

            // Register repositories
            services.AddSingleton<IGameRepository, GameRepository>();
            // Register services
            services.AddScoped<IGameService, GameService>();
            return services;
        }
    }
}
