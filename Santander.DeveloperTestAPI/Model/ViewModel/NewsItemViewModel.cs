namespace Santander.DeveloperTestAPI.Model.ViewModel
{
    public class NewsItemViewModel
    {
        public NewsItemViewModel(NewsItem item)
        {
            if(item != null)
            {
                Title = item.Title;
                Uri = string.IsNullOrWhiteSpace(item.Url) ? null : new Uri(item.Url);
                PostedBy = item.By;
                Score = item.Score;
                CommentCount = item.CommentCount;
                Time = item.FromUnixTimestamp;
            }
         
        }
        public string? Title { get; }
        public Uri? Uri { get; }
        public string? PostedBy { get; }
        public DateTime Time { get; }
        public int Score { get; }
        public int CommentCount { get; }
   
    }
}
