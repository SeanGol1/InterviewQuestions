using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using InterviewQuestionsAPI.Models;

namespace InterviewQuestionsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OpenAIController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public OpenAIController(IConfiguration config)
        {
            _config = config;
            _httpClient = new HttpClient();
        }

        [HttpPost("generate-questions")]
        public async Task<IActionResult> GenerateQuestions([FromBody] JobRequest request)
        {
            string apiKey = _config["OpenAI:ApiKey"];
            string prompt = $"Generate 5 interview questions based on the following job description:\n\n{request.JobDescription}";

            var body = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                max_tokens = 400,
                temperature = 0.7
            };

            var requestContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", requestContent);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            var responseContent = await response.Content.ReadAsStringAsync();
            return Ok(JsonDocument.Parse(responseContent));
        }
    }
}
