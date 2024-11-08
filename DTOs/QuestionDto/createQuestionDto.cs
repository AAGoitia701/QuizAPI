using System.Text.Json.Serialization;

namespace QuizAPI.DTOs.QuestionDto
{
    public class CreateQuestionDto
    {
        public string QuestionQuiz { get; set; }
        public string Answer { get; set; }
    }
}
