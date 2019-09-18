using AspNetCore.WebApi.Core.FrameworkBase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace AspNetCore.WebApi.Core.Infrastructure
{
    public class SystemStartup
    {
        public static void LoadConfig(IConfiguration configuration)
        {
            FrameworkConst.HospitalAssembly = configuration.GetSection("Hospital").Value;
        }

        public static void InitializeIoc(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            //找到所有继承IDependency的Assemblies
            //var assemblies = Assembly
            //    .GetEntryAssembly()
            //    .GetReferencedAssemblies()
            //    .Select(Assembly.Load)
            //    //.SelectMany(x => x.DefinedTypes)
            //    //.Where(type => typeof(IDependency).GetTypeInfo().IsAssignableFrom(type.AsType()))
            //    .ToArray();



            Assembly[] assemblies =
            {
                Assembly.Load("AspNetCore.WebApi.Core"),
                Assembly.Load("AspNetCore.WebApi.IService"),
                Assembly.Load("AspNetCore.WebApi.Service"),
                Assembly.Load("AspNetCore.WebApi.ShangHai10Yuan"),
            };

            //找到Assemblies中的所有接口
            var types = assemblies
                .SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(IDependency))))
                .ToArray();


            InitServices(types, serviceCollection);
        }

        private static void InitServices(Type[] depends, IServiceCollection serviceCollection)
        {
            var serviceInterfaces =
                depends.Where(p => typeof(IServiceBase).IsAssignableFrom(p) && p.IsInterface && p != typeof(IServiceBase))
                    .ToArray();
            foreach (var serviceInterface in serviceInterfaces)
            {
                var sub =
                    depends.Where(p => serviceInterface.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                        .ToArray();
                var destType = sub.FirstOrDefault(p => p.Assembly.GetName().Name == FrameworkConst.HospitalAssembly);
                var first = destType ??
                            sub.OrderBy(p => p.FullName?.Length).FirstOrDefault();
                if (first != null)
                    serviceCollection.AddScoped(serviceInterface, first);
            }
            Console.WriteLine($"[系统初始化] 注册Service，共计:{serviceInterfaces.Length}个");
        }
    }
}
