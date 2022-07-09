using MediatR;
using System.Reflection;

namespace WorldIndexesComparer.BackgroundServices.Configurations
{
    public static class CQRSConfiguration
    {
        public static IServiceCollection AddCQRSConfiguration(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddMediatR(typeof(Application.Extensions.ServiceCollectionExtensions).Assembly);

            return services;
        }
    }
}