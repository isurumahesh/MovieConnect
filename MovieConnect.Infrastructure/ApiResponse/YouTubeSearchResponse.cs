namespace MovieConnect.Infrastructure.ApiResponse
{
    public class YouTubeSearchResponse
    {
        public string Kind { get; set; } = string.Empty;
        public List<YouTubeItem> Items { get; set; } = new List<YouTubeItem>();
    }
}