using AutoMapper;
using MovieConnect.Core.Models;
using MovieConnect.Infrastructure.ApiResponse;

namespace MovieConnect.Infrastructure
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<OmdbMovieResponse, MovieDetail>();
            CreateMap<OmdbRating, Rating>();

            CreateMap<YouTubeItem, MovieVideo>()
            .ForMember(dest => dest.VideoId, opt => opt.MapFrom(src => src.Id != null ? src.Id.VideoId : string.Empty))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Snippet != null ? src.Snippet.Title : string.Empty))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Snippet != null ? src.Snippet.Description : string.Empty))
            .ForMember(dest => dest.ChannelTitle, opt => opt.MapFrom(src => src.Snippet != null ? src.Snippet.ChannelTitle : string.Empty))
            .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom(src =>
                src.Id != null && !string.IsNullOrEmpty(src.Id.VideoId)
                    ? new Uri($"https://www.youtube.com/watch?v={src.Id.VideoId}")
                    : null));

            CreateMap<YouTubeSearchResponse, List<MovieVideo>>()
                .ConvertUsing((src, dest, context) =>
                    src.Items != null
                        ? src.Items.Select(item => context.Mapper.Map<MovieVideo>(item)).ToList()
                        : new List<MovieVideo>());
        }
    }
}