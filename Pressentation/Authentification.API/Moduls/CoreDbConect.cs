using Microsoft.EntityFrameworkCore;
using Authentification.Data.Context;

namespace Authentification.API.Moduls
{
    public static class CoreDbConect
    {
        public static IServiceCollection AddDb(this IServiceCollection services, ConfigurationManager configuration)
        {
            configuration.AddUserSecrets<AuthentificationContext>().Build();

            services.AddDbContext<AuthentificationContext>(options =>
                {
                    var conectionSrtring = configuration.GetConnectionString("EfCoreAuthentificationDataBase");

                    options.UseSqlServer(conectionSrtring);
                });

            return services;
        }
    }
}
