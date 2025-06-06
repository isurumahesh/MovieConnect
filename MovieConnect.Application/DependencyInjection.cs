using Microsoft.Extensions.DependencyInjection;
using MovieConnect.Application.Interfaces;
using MovieConnect.Application.Services;

namespace MovieConnect.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });

            services.AddScoped<IMovieDetailServiceSelector, MovieDetailServiceSelector>();
            services.AddScoped<IMovieVideoServiceSelector, MovieVideoServiceSelector>();

            return services;
        }
    }
}