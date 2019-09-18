using AspNetCore.WebApi.IService;
using System.Threading.Tasks;

namespace AspNetCore.WebApi.Service
{
    public class DemoService : IDemoService
    {
        public string GetString(string text)
        {
            return text;
        }

        public async Task NoReturnTask()
        {
            return;
        }

        public int key { get; set; } = 1;
    }
}
