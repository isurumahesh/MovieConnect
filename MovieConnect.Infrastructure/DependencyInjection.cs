using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieConnect.Application.Interfaces;
using MovieConnect.Core.Interfaces;
using MovieConnect.Infrastructure.Configurations;
using MovieConnect.Infrastructure.Extensions;
using MovieConnect.Infrastructure.Services;
using Serilog;
using Serilog.Formatting.Json;

namespace MovieConnect.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
           .ReadFrom.Configuration(configuration)
           .MinimumLevel.Information()
           .WriteTo.Console()
           .WriteTo.File(new JsonFormatter(), "logs/log-.txt", rollingInterval: RollingInterval.Day)
           .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog();
            });

            services.AddMemoryCache();
            services.AddMovieProviderHttpClients();
            services.Configure<MovieProviderOptions>(configuration.GetSection("MovieProvidersOptions"));
            services.AddScoped<IMovieDetailService>(sp => sp.GetRequiredService<OmdbMovieDetailService>());
            services.AddScoped<IMovieVideoService>(sp => sp.GetRequiredService<YouTubeVideoService>());
            services.AddScoped<IMovieProviderConfiguration, MovieProviderConfiguration>();
            services.AddSingleton<ICacheService, MemoryCacheService>();

            services.AddAutoMapper(typeof(ApiMappingProfile));

            return services;
        }
    }
}