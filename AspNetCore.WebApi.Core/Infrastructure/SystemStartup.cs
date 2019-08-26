using AspNetCore.WebApi.Core.FrameworkBase;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace AspNetCore.WebApi.Core.Infrastructure
{
    public class SystemStartup
    {
        public static void InitializeIoc(IServiceCollection serviceCollection)
        {
            //找到所有继承IDependency的Assemblies
            //var ass = Assembly.Load("AspNetCore.WebApi.IService.dll").GetTypes();
            var assemtestblies = AppDomain.CurrentDomain.GetAssemblies().Where(p => p.FullName.StartsWith("AspNetCore.WebApi"));
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IDependency))))
                .ToArray();

            //找到Assemblies中的所有接口
            var interfaces = assemblies.Where(p => p.IsInterface).ToArray();

            foreach (var serviceInterface in interfaces)
            {
                //找到所有serviceInterface的实现实例
                var serviceInstances = assemblies
                    .Where(p => serviceInterface.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                    .ToArray();
                if (serviceInstances.Length == 0)
                    continue;
                foreach (var serviceInstance in serviceInstances)
                {
                    //注册接口和实例
                    serviceCollection.AddScoped(serviceInterface, serviceInstance);
                }
            }

        }
    }
}
