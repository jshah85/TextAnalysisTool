using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace TextAnalysisTool {
    public sealed class CacheProvider {

        private static readonly Object SyncRoot = new Object();

        private static volatile CacheProvider _instance;

        private static readonly MemoryCache _cache = MemoryCache.Default;

        private CacheProvider() {}

        public static CacheProvider Instance {
            get {
                if (_instance != null) {
                    return _instance;
                }
                lock (SyncRoot) {
                    if (_instance == null) {
                        _instance = new CacheProvider();
                    }
                }
                return _instance;
            }
        }

        public void AddToCache(List<CacheItem> definitions) {
            var cacheItemPolicy = new CacheItemPolicy {
                AbsoluteExpiration = DateTimeOffset.MaxValue
            };

            foreach (var definition in definitions.Where(definition => !_cache.Contains(definition.Key))) {
                _cache.Add(definition.Key, definition.Value, cacheItemPolicy);
            }
        }

        //Get stuff from the cache
        public List<string> GetFromCache(string key) {
            return _cache.Get(key) as List<string>;
        }
    }
}
