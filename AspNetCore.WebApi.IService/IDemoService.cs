using AspNetCore.WebApi.Core.FrameworkBase;
using System.Threading.Tasks;

namespace AspNetCore.WebApi.IService
{
    public interface IDemoService : IServiceBase
    {
        string GetString(string text);

        Task NoReturnTask();
    }
}
