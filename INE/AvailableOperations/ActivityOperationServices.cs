namespace c_basic_api.INE.AvailableOperations;

public static class ActivityOperationServices
{
    public static void RegisterActivityOperations(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient("QueryOperationsAvailable", client => 
            client.BaseAddress = new Uri(""));
    }
}