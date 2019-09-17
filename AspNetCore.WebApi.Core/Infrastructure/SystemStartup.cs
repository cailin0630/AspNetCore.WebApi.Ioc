using AspNetCore.WebApi.Core.FrameworkBase;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace AspNetCore.WebApi.Core.Infrastructure
{
    public class SystemStartup
    {
        public static void InitializeIoc(IServiceCollection serviceCollection)
        {
            //找到所有继承IDependency的Assemblies
            Assembly[] assemblies =
            {
                Assembly.Load("AspNetCore.WebApi.Core"),
                Assembly.Load("AspNetCore.WebApi.IService"),
                Assembly.Load("AspNetCore.WebApi.Service")
            };
            //var iservices = Assembly.Load("AspNetCore.WebApi.IService").GetTypes();
            //var services = Assembly.Load("AspNetCore.WebApi.IService").GetTypes();
            //var assemtestblies = AppDomain.CurrentDomain.GetAssemblies().Where(p => p.FullName.StartsWith("AspNetCore.WebApi"));
            //var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            //    .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IDependency))))
            //    .ToArray();

            //找到Assemblies中的所有接口
            var types = assemblies
                .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IDependency))))
                .ToArray();
            var tmpTypes = types.Where(_ => _.IsClass);
            var didatas = types.Where(t => t.IsInterface)
                .Select(t => new
                {
                    serviceType = t,
                    implementationType = tmpTypes.FirstOrDefault(c => c.GetInterfaces().Contains(t))
                }).ToList();

            didatas.ForEach(t =>
            {
                if (t.implementationType != null)
                    serviceCollection.AddScoped(t.serviceType, t.implementationType);
            });


        }
    }
}
