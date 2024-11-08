using Microsoft.AspNetCore.Mvc;
using QuizAPI.DTOs.QuestionDto;
using QuizAPI.Models;

namespace QuizAPI.Respository.IRepository
{
    public interface IQuizRepository
    {
        public Task<List<Question>> GetAllAsync();
        public Task<Question> GetOneAsync(int id);
        //public Task<IActionResult> GetOneRandomAsync();
        public Task<Question> CreateAsync(Question q);

        public Task<Question> UpdateAsync(int id, UpdateQDto updateDto);
    }
}
