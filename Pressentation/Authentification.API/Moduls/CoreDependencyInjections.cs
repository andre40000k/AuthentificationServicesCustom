using Authentification.Application.Interfaces;
using Authentification.Data.Repository;

namespace Authentification.API.Moduls
{
    public static class CoreDependencyInjections
    {
        public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
        {
            services.AddTransient<IRepositories, CRUDOperations>();

            return services;
        }
    }
}
