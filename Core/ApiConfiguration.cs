namespace c_basic_api.Core;
using Microsoft.Extensions.Configuration;

public class ApiConfiguration
{
    public static void Start(IConfiguration configuration)
    {
        string? url = configuration["INEApi:AvailableOperations"];
        
    }
}