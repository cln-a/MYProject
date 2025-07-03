namespace Application.RestSharp
{
    public interface IRestSharpService
    {
        Task<string> ExcuteAsync(string url, string method, object? body = null, Dictionary<string, string>? headers = null);
    }
}
