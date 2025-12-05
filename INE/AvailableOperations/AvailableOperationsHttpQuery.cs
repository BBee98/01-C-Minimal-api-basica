namespace c_basic_api.INE.AvailableOperations;
using Core;

public class AvailableOperationsHttpQuery: IQuery<IActivityOperationModel[]>

{
    public IActivityOperationModel[] Execute(IHttpClientFactory httpClientFactory)
    {
        HttpClient client = httpClientFactory.CreateClient("QueryOperationsAvailable");
        return Array.Empty<IActivityOperationModel>();
    }
}