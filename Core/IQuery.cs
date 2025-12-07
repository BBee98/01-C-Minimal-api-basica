namespace c_basic_api.Core;

public interface IQuery<T>
{
    public Task<T> Execute(IHttpClientFactory httpClientFactory);
}