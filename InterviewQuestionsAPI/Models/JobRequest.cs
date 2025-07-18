namespace InterviewQuestionsAPI.Models
{
    public class JobRequest
    {
        public string JobDescription { get; set; }
    }
    public class OpenAiResponse
    {
        public List<string> Questions { get; set; }
    }
    public class GradingItem
    {
        public string Question { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public int Score { get; set; } = 100;
        public string Feedback { get; set; } = string.Empty;
    }
}
