using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CacheInMemoryAspNetCoreWeb
{
    public static class CacheKeys
    {
        public static string Entry { get { return "_Entry"; } }
        public static string CallbackEntry { get { return "_Callback"; } }
        public static string CallbackMessage { get { return "_CallbackMessage"; } }
    }
}
