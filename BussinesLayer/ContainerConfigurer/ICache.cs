public interface ICache
{
    T Get<T>(string key);
    void Set(string key, object objectData, int? cacheTime = null);
    bool IsSet(string key);
    void Remove(string key);
    void RemoveByPattern(string pattern);
    void Clear();
}