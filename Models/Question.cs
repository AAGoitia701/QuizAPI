using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace QuizAPI.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        [JsonPropertyName("question")]
        public string QuestionQuiz { get; set; }
        [JsonPropertyName("answer")]
        public string Answer { get; set; }

    }
}
