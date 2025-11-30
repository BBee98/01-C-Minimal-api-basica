namespace c_basic_api.Core;

public interface IQuery<out T>
{
    public T Execute(IHttpClientFactory httpClientFactory);
}