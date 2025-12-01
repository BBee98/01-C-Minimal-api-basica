using c_basic_api.Core;

namespace c_basic_api.INE.AvailableOperations;

public static class ActivityOperationServices
{
    public static void RegisterActivityOperations(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IQuery<IActivityOperationModel[]>, AvailableOperationsHttpQuery>();
        serviceCollection.AddHttpClient("QueryOperationsAvailable", client => 
            client.BaseAddress = new Uri("https://servicios.ine.es/wstempus/js/ES/OPERACIONES_DISPONIBLES"));
    }
}