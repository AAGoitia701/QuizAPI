using QuizAPI.DTOs.QuestionDto;
using QuizAPI.Models;

namespace QuizAPI.Mappers
{
    public static class QuestionMapper
    {
        public static QuestionDto FromQToQDto(this Question q)
        {
            return new QuestionDto
            {
                QuestionQuiz = q.QuestionQuiz,
                Answer = q.Answer,
                Id = q.Id
            };
        }

        public static Question FromDtoToQ(this CreateQuestionDto createDto)
        {
            return new Question
            {
                QuestionQuiz = createDto.QuestionQuiz,
                Answer = createDto.Answer,
            };
        }


    }
}
