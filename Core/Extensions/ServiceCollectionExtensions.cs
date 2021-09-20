using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Extensions
{
    //extension classes are always static
    public static class ServiceCollectionExtensions
    {
        //Creates a service colleciton of all the dependency injections
        // this = what do you want to extend?
        // Icodemodule array = we can give all type of modules that are using the interface ICoreModule
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceColelction, ICoreModule[] modules)
        {
            // for each given module, load the module
            foreach (var module in modules)
            {
                module.Load(serviceColelction);
            }
            return ServiceTool.Create(serviceColelction);
        }
    }
}
