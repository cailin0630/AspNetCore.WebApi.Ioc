using AspNetCore.WebApi.Core.FrameworkBase;

namespace AspNetCore.WebApi.IService
{
    public interface IDemoService : IServiceBase
    {
        string GetString(string text);
    }
}
