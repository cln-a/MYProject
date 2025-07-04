using RestSharp;

namespace Application.RestSharp
{
    public interface IRestSharpService
    {
        Task<string> ExecuteAsync(string url, Method method, object? body = null);
    }
}
