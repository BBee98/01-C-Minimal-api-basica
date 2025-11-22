namespace c_basic_api.Core.Configuration;

public class ConfigureServices
{
    public void Add(IServiceCollection services)
    {
        services.AddHttpClient();
    }
}