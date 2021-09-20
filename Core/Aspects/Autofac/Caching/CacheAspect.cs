using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Core.Aspects.Autofac.Caching
{
    public class CacheAspect : MethodInterception
    {
        private int _duration;
        private ICacheManager _cacheManager;

        // after 60 min the cache will be deleted
        public CacheAspect(int duration = 60)
        {
            _duration = duration;
            // dependency injection
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        public override void Intercept(IInvocation invocation)
        {
            //invocation = method
            // get the name space of the method (GetAll,add,remove,..) + the class. Get the name of the method(Get,getall,add,..)
            // example: Business.Abstract.IProductService.GetAll
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
            // arguments = parameters of the method 
            var arguments = invocation.Arguments.ToList();
            // creating the key with the methodname and add the arguments as params. But if there is no params then set Null
            // x?.Tostring= if there is than use this, ?? = else use NULL
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";
            // check if this key is available in the cache
            if (_cacheManager.IsAdd(key))
            {
                // get the data from cach
                invocation.ReturnValue = _cacheManager.Get(key);
                return;
            }
            // continue the method running
            // get the data from database
            invocation.Proceed();
            // add this cachevalue to the cache
            _cacheManager.Add(key, invocation.ReturnValue, _duration);
        }
    }
}
