namespace c_basic_api.INE.AvailableOperations;

public class AvailableOperationsHttpQuery
{
    public void Execute(IServiceCollection services)
    {
        services.AddHttpClient(client =>
        {
            client.BaseAddress = new Uri("https://servicios.ine.es/wstempus/js/ES/OPERACIONES_DISPONIBLES");
        });
    }
}