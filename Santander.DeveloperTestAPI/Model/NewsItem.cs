namespace Santander.DeveloperTestAPI.Model
{
    public class NewsItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Url { get; set; }
        public string? By { get; set; }
        public int Score  { get; set; }
        public long Time  { get; set; }
        public int[]? Kids { get; set; }

        public int CommentCount => Kids != null ? Kids.Length : 0;

        public DateTime FromUnixTimestamp =>
             new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(Time).ToLocalTime();
    }
}
