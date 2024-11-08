using System.Text.Json.Serialization;

namespace QuizAPI.DTOs.QuestionDto
{
    public class QuestionDto
    {
        public int Id { get; set; }
        [JsonPropertyName("question")]
        public string QuestionQuiz { get; set; }
        [JsonPropertyName("answer")]
        public string Answer { get; set; }
    }
}
