using System;
using System.IO;
using System.Runtime.Caching;
using System.Runtime.Serialization.Formatters.Binary;

public class MemoryCacheService : ICache
{
    private readonly int _cacheTime;

    public MemoryCacheService(int cacheTime = 60) // Cache-ul expiră după 60 de minute implicit
    {
        _cacheTime = cacheTime;
    }

    protected ObjectCache Cache
    {
        get { return MemoryCache.Default; }
    }

    // Obține valoarea asociată cu cheia specificată
    public T Get<T>(string key)
    {
        if (IsSet(key))
        {
            BinaryFormatter deserializer = new BinaryFormatter();
            using (MemoryStream memStream = new MemoryStream((byte[])Cache[key]))
            {
                return (T)deserializer.Deserialize(memStream);
            }
        }
        return default(T);
    }

    // Adaugă o valoare în cache
    public void Set(string key, object objectData, int? cacheTime = null)
    {
        if (objectData == null)
        {
            return;
        }

        var policy = new CacheItemPolicy();
        if (!cacheTime.HasValue)
        {
            cacheTime = _cacheTime; // Folosește cache-ul standard dacă nu se specifică un timp
        }

        policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime.Value);

        BinaryFormatter serializer = new BinaryFormatter();
        using (MemoryStream memStream = new MemoryStream())
        {
            serializer.Serialize(memStream, objectData);
            Cache.Add(new CacheItem(key, memStream.ToArray()), policy);
        }
    }

    // Verifică dacă există o valoare în cache pentru cheia specificată
    public bool IsSet(string key)
    {
        return Cache.Contains(key);
    }

    // Îndepărtează o valoare din cache
    public void Remove(string key)
    {
        Cache.Remove(key);
    }

    // Șterge toate valorile care se potrivesc cu un pattern
    public void RemoveByPattern(string pattern)
    {
        foreach (var item in Cache)
        {
            if (item.Key.StartsWith(pattern))
            {
                Remove(item.Key);
            }
        }
    }

    // Curăță tot cache-ul
    public void Clear()
    {
        foreach (var item in Cache)
        {
            Remove(item.Key);
        }
    }
}
