using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MovieConnect.Infrastructure.Configurations;
using MovieConnect.Infrastructure.Services;

namespace MovieConnect.Infrastructure.Extensions
{
    public static class MovieProviderHttpClientExtensions
    {
        public static IServiceCollection AddMovieProviderHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient<OmdbMovieDetailService>((provider, client) =>
            {
                var omdbProvider = GetMovieDetailProvider(provider, "omdb");
                if (omdbProvider != null)
                {
                    client.BaseAddress = new Uri(omdbProvider.ApiUrl);
                }
            })
            .AddPolicyHandler((provider, _) =>
            {
                var omdbProvider = GetMovieDetailProvider(provider, "omdb");
                return HttpClientPolicyBuilder.GetPolicy(omdbProvider?.HttpClientPolicies);
            });

            services.AddHttpClient<YouTubeVideoService>((provider, client) =>
            {
                var youtubeProvider = GetMovieVideoProvider(provider, "youtube");
                if (youtubeProvider != null)
                {
                    client.BaseAddress = new Uri(youtubeProvider.ApiUrl);
                }
            })
            .AddPolicyHandler((provider, _) =>
            {
                var youtubeProvider = GetMovieVideoProvider(provider, "youtube");
                return HttpClientPolicyBuilder.GetPolicy(youtubeProvider?.HttpClientPolicies);
            });

            return services;
        }

        private static MovieDetailProvider? GetMovieDetailProvider(IServiceProvider provider, string name)
        {
            var options = provider.GetRequiredService<IOptions<MovieProviderOptions>>().Value;
            return options.MovieDetailProviders.FirstOrDefault(p =>
                p.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && p.IsActive);
        }

        private static MovieVideoProvider? GetMovieVideoProvider(IServiceProvider provider, string name)
        {
            var options = provider.GetRequiredService<IOptions<MovieProviderOptions>>().Value;
            return options.MovieVideoProviders.FirstOrDefault(p =>
                p.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && p.IsActive);
        }
    }
}