using c_basic_api.Core;

namespace c_basic_api.INE.AvailableOperations;

public static class AvailableOperationsServices
{
    public static void RegisterAvailableOperations(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IQuery<IAvailableOperationsModel[]>, AvailableOperationsHttpQuery>();
        serviceCollection.AddHttpClient("QueryAvailableOperations", client => 
            client.BaseAddress = new Uri("https://servicios.ine.es/wstempus/js/ES/OPERACIONES_DISPONIBLES"));
    }
}