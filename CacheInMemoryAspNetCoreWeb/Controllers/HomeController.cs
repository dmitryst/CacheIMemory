using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CacheInMemoryAspNetCoreWeb.Controllers
{
    public class HomeController : Controller
    {
        private IMemoryCache _cache;

        public HomeController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public IActionResult CacheTryGetValueSet()
        {
            DateTime cacheEntry;

            // look for cache key
            if (!_cache.TryGetValue(CacheKeys.Entry, out cacheEntry))
            {
                // key not in cache, so get data
                cacheEntry = DateTime.Now;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    // keep in cache for this time, reset time if accessed
                    .SetSlidingExpiration(TimeSpan.FromSeconds(5));

                // save data in cache
                _cache.Set(CacheKeys.Entry, cacheEntry, cacheEntryOptions);
            }

            return View("Cache", cacheEntry);
        }

        public IActionResult CacheGet()
        {
            // fetch the cached time
            var cacheEntry = _cache.Get<DateTime?>(CacheKeys.Entry);
            return View("Cache", cacheEntry);
        }

        public IActionResult CacheGetOrCreate()
        {
            var cacheEntry = _cache.GetOrCreate(CacheKeys.Entry, entry => {
                entry.SlidingExpiration = TimeSpan.FromSeconds(5);
                return DateTime.Now;
            });

            return View("Cache", cacheEntry);
        }

        public async Task<IActionResult> CacheGetOrCreateAsync()
        {
            var cacheEntry = await _cache.GetOrCreateAsync(CacheKeys.Entry, entry => {
                entry.SlidingExpiration = TimeSpan.FromSeconds(5);
                return Task.FromResult(DateTime.Now);
            });

            return View("Cache", cacheEntry);
        }

        public IActionResult CacheRemove()
        {
            _cache.Remove(CacheKeys.Entry);
            return RedirectToAction("CacheGet");
        }
    }
}