using InterviewQuestionsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Buffers.Text;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            string jobDescription = request.JobDescription;
            string prompt = @$"You are an interview assistant. Based on the following job description, generate 5 clear and distinct interview questions that a recruiter might ask. Number them.

            Job Description:
            {jobDescription}

            Please output in JSON format like this:
            [
              ""1. Question 1?"",
              ""2. Question 2?"",
              ...
            ]";


            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                temperature = 0.7
            };

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", Environment.GetEnvironmentVariable("OPENAI_API_KEY"));

            var response = await httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);
            var json = await response.Content.ReadAsStringAsync();

            dynamic result = JsonConvert.DeserializeObject(json);
            string responseText = result.choices[0].message.content;

            var questions = JsonConvert.DeserializeObject<List<string>>(responseText);

            return Ok(new { Questions = questions });
        }

        [HttpPost("grade-answers")]
        public async Task<IActionResult> GradeAnswers([FromBody] List<GradingItem> items)
        {
            string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");

            var promptBuilder = new StringBuilder();
            for (int i = 0; i < items.Count; i++)
            {
                promptBuilder.AppendLine($"Question {i + 1}: {items[i].Question}");
                promptBuilder.AppendLine($"Answer: {items[i].Answer}");
                promptBuilder.AppendLine();
            }

            promptBuilder.AppendLine("Please return a JSON array in this format:");
            promptBuilder.AppendLine(@"[
                  {
                    ""question"": ""..."",
                    ""answer"": ""..."",
                    ""score"": 7,
                    ""feedback"": ""...""
                  }
                ]");

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
            new { role = "system", content = "You are an interview coach. Grade each answer from 1 to 10 and give brief feedback." },
            new { role = "user", content = promptBuilder.ToString() }
        },
                temperature = 0.7
            };

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);

            var response = await httpClient.PostAsJsonAsync("https://api.openai.com/v1/chat/completions", requestBody);
            var json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, json);
            }

            dynamic result = JsonConvert.DeserializeObject(json);
            string content = result.choices[0].message.content;

            try
            {
                // Try to parse GPT response into usable objects
                var gradedItems = JsonConvert.DeserializeObject<List<GradingItem>>(content);
                //return Ok(new { Graded = gradedItems });
                return Ok(new { graded = gradedItems });
            }
            catch
            {
                // If GPT returns invalid JSON
                return Ok(new { Raw = content });
            }
        }

    }
}
