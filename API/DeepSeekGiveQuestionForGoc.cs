using LabApi.Features.Console;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.API
{
    public class DeepSeekGiveQuestionForGoc
    {
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://api.deepseek.com/v1/chat/completions";
        public async Task<string> ChatAsync(string userMessage)
        {
            var requestBody = new
            {
                model = "deepseek-chat",
                messages = new[]
                {
                    new { role = "user", content = userMessage }
                },
                temperature = 0.7,
                max_tokens = 2000
            };
            string json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(ApiUrl, content);
            string responseJson = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Logger.Error($"API 请求失败: {response.StatusCode} - {responseJson}");
                throw new Exception($"API 错误: {responseJson}");
            }
            var result = JObject.Parse(responseJson);
            string reply = result["choices"][0]["message"]["content"].ToString();

            return reply;
        }
    }
}
