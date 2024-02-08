using Santander.DeveloperTestAPI.Model;

namespace Santander.DeveloperTestAPI.Services
{
    public interface INewsGetterService
    {
        Task<IEnumerable<NewsItem>> GetItems(int howMany);
    }
}