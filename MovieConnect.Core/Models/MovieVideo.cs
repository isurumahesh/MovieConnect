namespace MovieConnect.Core.Models
{
    public class MovieVideo
    {
        public string VideoId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ChannelTitle { get; set; } = string.Empty;
        public Uri? VideoUrl { get; set; }
    }
}