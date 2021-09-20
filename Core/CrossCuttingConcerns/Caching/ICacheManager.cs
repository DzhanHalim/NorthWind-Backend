using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Caching
{
   public interface ICacheManager
    {
        T Get<T>(string key);

        object Get(string key);

        void Add(string key, object value, int duration);

        // if it is in Cache than use it from cache if not than use it
        // from data but add it on cache.
        bool IsAdd(string key);

        // remove it from cache
        void Remove(string key);

        // all names that contain the given string will be remvoed
        void RemoveByPattern(string pattern);




    }
}
