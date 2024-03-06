namespace Tekton.Application.Services
{
	public interface ICacheRepository<T>
    {
        T Get(string key);
        T TryGetValue(string key);
        void Set(string key, T item, TimeSpan? expiry = null);
        void Remove(string key);
    }
}

