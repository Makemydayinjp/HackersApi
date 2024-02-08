using Newtonsoft.Json;
using Santander.DeveloperTestAPI.Cache;
using Santander.DeveloperTestAPI.Exceptions;
using Santander.DeveloperTestAPI.Model;

namespace Santander.DeveloperTestAPI.Services
{
    public class NewsGetterService : INewsGetterService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalCache _localCache;

        public NewsGetterService(IHttpClientFactory httpClientFactory, ILocalCache localCache)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _localCache = localCache ?? throw new ArgumentNullException(nameof(localCache));
        }

        public async Task<IEnumerable<NewsItem>> GetItems(int howMany)
        {

            var ids = (await  GetNewsIds()).Take(howMany);

            var client = _httpClientFactory.CreateClient("hackerNewsAPI");
            var items = new List<NewsItem>();
            foreach (var newsId in ids)
            {
                NewsItem? convertedItem;
                if(!_localCache.TryGet(newsId, out convertedItem))
                {
                    var newsItem =  await client.GetAsync($"item/{newsId}.json");
                    if (!newsItem.IsSuccessStatusCode)
                        continue;

                    convertedItem = await newsItem.Content.ReadFromJsonAsync<NewsItem>();
                    _localCache.Add(newsId, convertedItem);
                }   
             
                if (convertedItem != null)
                    items.Add(convertedItem);
            }

            if (items.Count == 0)
            {
                throw new CouldNotFetchNewsException($"Could not fetch newsitems for the following ids: " +
                    $"{JsonConvert.SerializeObject(ids)}.");
            }

            return items;
        }

        private async Task<List<int>> GetNewsIds()
        {
            var client = _httpClientFactory.CreateClient("hackerNewsAPI");

            var idsResp = await client.GetAsync("beststories.json");

            if (!idsResp.IsSuccessStatusCode)
                throw new CouldNotFetchNewsException("Could not fetch ids.");

            var ids = await idsResp.Content.ReadFromJsonAsync<List<int>>();
            if (ids == null || ids.Count == 0)
            {
                throw new CouldNotFetchNewsException("Did not find any ids to fetch.");
            }

            return ids;
        }


    }
}
