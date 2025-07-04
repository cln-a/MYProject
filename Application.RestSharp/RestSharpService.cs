using Application.Common;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace Application.RestSharp
{
    public class RestSharpService : IRestSharpService
    {
        private readonly RestClient _client;
        private readonly ILogger _logger;
        private readonly RestSharpOptions _restSharpOptions;

        public RestSharpService(ILogger logger, RestSharpOptions options)
        {
            this._logger = logger;  
            this._restSharpOptions = options;

            _client = new RestClient(_restSharpOptions.BaseUrl);
        }

        public async Task<string> ExecuteAsync(string url, Method method, object? body = null)
        {
            try
            {
                var request = new RestRequest(url, method);
                foreach (var header in _restSharpOptions.DefaultHeader)
                {
                    request.AddHeader(header.Key, header.Value);
                }
                if (!string.IsNullOrEmpty(_restSharpOptions.Apikey)) 
                {
                    request.AddHeader("Authorization", $"Bearer {_restSharpOptions.Apikey}");
                }
                if (body != null)
                {
                    request.AddJsonBody(body);
                }
                var response = await _client.ExecuteAsync(request);
                
                return response.Content ?? string.Empty;
            }
            catch (Exception ex) 
            {
                _logger.LogDebug(ex, "Error executing REST request");
                return string.Empty;    
            }
        }
    }
}
