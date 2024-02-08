
namespace Santander.DeveloperTestAPI.Cache
{
    public interface ILocalCache
    {
        void Add<TOut>(int key, TOut? outValue, TimeSpan? timeSpan = null) where TOut : class, new();
        bool TryGet<TOut>(int key, out TOut? outValue) where TOut : class, new();
    }
}