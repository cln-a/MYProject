using Application.RestSharp;
using Microsoft.Extensions.Logging;
using RestSharp;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Application.ArtificialIntelligence
{
    public class ArtificialIntelligenceViewModel : BindableBase
    {
        private DelegateCommand _requestCommand;
        private DelegateCommand _clearCommand;
        private readonly IRestSharpService _restSharpService;
        private readonly ILogger _logger;
        private string _question = "如何学习halcon";
        private string _requestResult;
        private ObservableCollection<string> _modelList 
            = new ObservableCollection<string>() { "Qwen/QwQ-32B", "Qwen/Qwen-14B", "Qwen/Qwen-7B" };
        private string _selectedModel = "Qwen/QwQ-32B";
        private string _systemPrompt = "你是一个资深的视觉工程师";
        private int _maxTokens = 512;
        private double _temperature = 0.7;
        private double _topP = 0.7;
        private double _topK = 50;

        public IRestSharpService RestSharpService => _restSharpService;
        public ILogger Logger => _logger;
        public string Question { get => _question; set => SetProperty(ref _question, value); }
        public string RequestResult { get => _requestResult; set => SetProperty(ref _requestResult, value); }
        public ObservableCollection<string> ModelList { get => _modelList; set => SetProperty(ref _modelList, value); }
        public string SelectedModel { get => _selectedModel; set => SetProperty(ref _selectedModel, value); }
        public string SystemPrompt { get => _systemPrompt; set => SetProperty(ref _systemPrompt, value); }
        public int MaxTokens { get => _maxTokens; set => SetProperty(ref _maxTokens, value); }
        public double Temperature { get => _temperature; set => SetProperty(ref _temperature, value); }
        public double TopP { get => _topP;set => SetProperty(ref _topP, value); }       
        public double TopK { get => _topK; set => SetProperty(ref _topK, value); }

        public DelegateCommand RequestCommand => _requestCommand ??= new DelegateCommand(RequestForAI);
        public DelegateCommand ClearCommand => _clearCommand ??= new DelegateCommand(() =>
        {
            this.RequestResult = string.Empty;
        });

        public ArtificialIntelligenceViewModel(IRestSharpService restSharpService, ILogger logger)
        {
            this._restSharpService = restSharpService;  
            this._logger = logger;  
        }

        private async void RequestForAI()
        {
            try
            {
                var body = @"{
" + "\n" +
                                          @"    ""model"": ""{model}"",
" + "\n" +
                                          @"    ""messages"": [
" + "\n" +
                                          @"        {
" + "\n" +
                                          @"            ""role"": ""user"",
" + "\n" +
                                          @"            ""content"": ""{contentSend}""
" + "\n" +
                                          @"        }
" + "\n" +
                                          @"    ],
" + "\n" +
                                          @"    ""stream"": false,
" + "\n" +
                                          @"    ""max_tokens"": {maxTokens},
" + "\n" +
                                          @"    ""stop"": null,
" + "\n" +
                                          @"    ""temperature"": {temperature},
" + "\n" +
                                          @"    ""top_p"": {topP},
" + "\n" +
                                          @"    ""top_k"": {topK},
" + "\n" +
                                          @"    ""frequency_penalty"": 0.5,
" + "\n" +
                                          @"    ""n"": 1,
" + "\n" +
                                          @"    ""response_format"": {
" + "\n" +
                                          @"        ""type"": ""text""
" + "\n" +
                                          @"    },
" + "\n" +
                                          @"    ""tools"": [
" + "\n" +
                                          @"        {
" + "\n" +
                                          @"            ""type"": ""function"",
" + "\n" +
                                          @"            ""function"": {
" + "\n" +
                                          @"                ""description"": ""<string>"",
" + "\n" +
                                          @"                ""name"": ""<string>"",
" + "\n" +
                                          @"                ""parameters"": {},
" + "\n" +
                                          @"                ""strict"": false
" + "\n" +
                                          @"            }
" + "\n" +
                                          @"        }
" + "\n" +
                                          @"    ]
" + "\n" +
                                          @"}";

                body = body.Replace("{model}", SelectedModel);
                body = body.Replace("{contentSend}", Question);
                body = body.Replace("{maxTokens}", MaxTokens.ToString());
                body = body.Replace("{temperature}", Temperature.ToString());
                body = body.Replace("{topP}", TopP.ToString());
                body = body.Replace("{topK}", TopK.ToString());
                var response = await _restSharpService.ExecuteAsync("/v1/chat/completions", Method.POST, body);
                using var doc = JsonDocument.Parse(response);
                var reasoningContent = doc.RootElement
                        .GetProperty("choices")[0]
                        .GetProperty("message")
                        .GetProperty("reasoning_content")
                        .GetString();
                RequestResult = reasoningContent ?? "未获取到推理内容";
            }
            catch (Exception ex) 
            {
                _logger.LogDebug($"API 请求异常{ex.Message}");
                RequestResult = "解析AI返回内容失败：" + ex.Message;
            }
        }
    }
}
