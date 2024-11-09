using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Data;
using QuizAPI.DTOs.QuestionDto;
using QuizAPI.Models;
using QuizAPI.Respository.IRepository;

namespace QuizAPI.Respository
{
    public class QuizRepository : IQuizRepository
    {
        private readonly ApplicationDbContext _context;
        public QuizRepository(ApplicationDbContext cont)
        {
            _context = cont;
        }
        


        public async Task<Question> GetOneAsync(int id)
        {
            var oneQuestion = await  _context.Questions.FirstOrDefaultAsync(r => r.Id == id);
            return oneQuestion;

        }

        public async Task<List<Question>> GetAllAsync()
        {
            var list = await _context.Questions.ToListAsync();
            return list;
        }

        public async Task<Question> CreateAsync(Question q)
        {
            await _context.Questions.AddAsync(q);
            await _context.SaveChangesAsync();
            return q;
        }

        public async Task<Question> UpdateAsync(int id, UpdateQDto updateDto )
        {
            var question = await _context.Questions.FirstOrDefaultAsync(r => r.Id == id);
            if (question == null) return null;
            
            question.QuestionQuiz = updateDto.QuestionQuiz;
            question.Answer = updateDto.Answer; 

            await _context.SaveChangesAsync();

            return question;
        }

        public async Task<Question?> DeleteAsync(int id)
        {
            var question = await _context.Questions.FirstOrDefaultAsync(r => r.Id == id);
            if (question == null) return null;

            _context.Remove(question);
            await _context.SaveChangesAsync();

            return question;
        }
    }
}
