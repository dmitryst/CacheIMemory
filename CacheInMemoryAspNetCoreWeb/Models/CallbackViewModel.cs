using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacheInMemoryAspNetCoreWeb.Models
{
    public class CallbackViewModel
    {
        public DateTime? CachedTime { get; set; }
        public string Message { get; set; }
    }
}
