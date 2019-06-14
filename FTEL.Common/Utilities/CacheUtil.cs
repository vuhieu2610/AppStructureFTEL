using System;
using System.Web;

namespace FTEL.Common.Utilities
{
    public class CacheUtil
    {
        private static readonly object CacheLockObject = new object();
        //public static readonly int CacheTime = Convert.ToInt32(ConfigurationManager.AppSettings["CacheTime"]);
        public static dynamic GetCacheObject(string cacheKey)
        {
            lock (CacheLockObject)
            {
                var result = HttpRuntime.Cache[cacheKey] as dynamic;
                return result;
            }
        }
        public static void InsertCacheObject(string cacheKey, dynamic dataCache, int CacheTimeMinutes = 0)
        {
            lock (CacheLockObject)
            {
                if (dataCache != null)
                {
                    //if (CacheTimeMinutes == 0)
                    //{
                    //    CacheTimeMinutes = int.Parse(ConfigUtil.CacheTimeMinutes);
                    //}
                    HttpRuntime.Cache.Insert(cacheKey, dataCache, null, DateTime.Now.AddMinutes(CacheTimeMinutes), TimeSpan.Zero);
                }
                //var result = HttpRuntime.Cache[cacheKey] as dynamic;
                //if (result != null) return result;
                //result = dataCache;
                //if (result != null)
                //{
                //    HttpRuntime.Cache.Insert(cacheKey, result, null, DateTime.Now.AddSeconds(CacheTimeMinutes), TimeSpan.Zero);
                //}
                //return result;
            }
        }
        public static void RemoveCacheObject(string cacheKey)
        {
            //foreach (System.Collections.DictionaryEntry entry in HttpContext.Current.Cache)
            //{
            //    HttpContext.Current.Cache.Remove((string)entry.Key);
            //}
            HttpRuntime.Cache.Remove(cacheKey);
        }
    }
}
