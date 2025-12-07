namespace c_basic_api.INE.AvailableOperations;
using Core;

public class AvailableOperationsHttpQuery: IQuery<IAvailableOperationsModel[]>

{
    public async Task<IAvailableOperationsModel[]> Execute(IHttpClientFactory httpClientFactory)
    {
        HttpClient client = httpClientFactory.CreateClient("QueryAvailableOperations");
        var json = await client.GetFromJsonAsync<List<AvailableOperationsDTO>>("");
        if (json is not null)
        {
            return json.ToArray<IAvailableOperationsModel>();
        }
        return Array.Empty<IAvailableOperationsModel>();
    }
}