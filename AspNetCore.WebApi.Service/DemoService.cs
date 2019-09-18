using AspNetCore.WebApi.IService;
using System.Threading.Tasks;

namespace AspNetCore.WebApi.Service
{
    public class DemoService : IDemoService
    {
        public virtual string GetString(string text)
        {
            return text;
        }

        public virtual async Task NoReturnTask()
        {
            return;
        }

    }
}
