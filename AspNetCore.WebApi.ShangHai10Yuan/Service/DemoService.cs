namespace AspNetCore.WebApi.ShangHai10Yuan.Service
{
    public class DemoService : AspNetCore.WebApi.Service.DemoService
    {
        public override string GetString(string text)
        {
            return $"{text}+123";
        }
    }
}
