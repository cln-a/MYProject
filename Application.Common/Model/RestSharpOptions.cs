namespace Application.Common
{
    public class RestSharpOptions
    {
        public string BaseUrl { get; set; } = "https://api.siliconflow.cn";

        public string? Apikey { get; set; } = "sk-nkcjeogckpoarhdpfeavqpzgnposmfajkgjicqevbnmjiehj";

        public int TimeOUt { get; set; } = 10000;

        public Dictionary<string, string> DefaultHeader { get; set; }
            = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","*/*" },
                {"User-Agent","Apifox/1.0.0 (https://apifox.com)" }
            };
    }
}
