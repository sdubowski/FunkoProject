using System;
using System.Reflection;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class InjectableAttribute : Attribute
{
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInjectables(this IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var injectableTypes = assembly.GetTypes()
                .Where(t => t.GetCustomAttribute<InjectableAttribute>() != null);

            foreach (var type in injectableTypes)
            {
                if (typeof(IMiddleware).IsAssignableFrom(type))
                {
                    services.AddTransient(type);
                }
                else
                {
                    var serviceType = type.GetInterfaces().FirstOrDefault();
                    if (serviceType != null)
                    {
                        services.AddScoped(serviceType, type);
                    }
                    else
                    {
                        services.AddScoped(type);
                    }
                }
            }
        }
        return services;
    }
}