using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Data;
using QuizAPI.DTOs.QuestionDto;
using QuizAPI.Mappers;
using QuizAPI.Models;
using QuizAPI.Respository.IRepository;
using SharpCompress.Common;
using System.Text.Json;

namespace QuizAPI.Controllers
{
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IQuizRepository _quizRepo;
        public QuizController(ApplicationDbContext context, IQuizRepository quiz)
        {
            _context = context;
            _quizRepo = quiz;
        }

        [HttpPost("json-upload")]
        public async Task<IActionResult> uploadJson([FromBody] string filePath)
        {
            // Filepath null or empty
            if (string.IsNullOrEmpty(filePath))
                return BadRequest("No path has been written");

            // File exists in directory
            if (!System.IO.File.Exists(filePath))
                return NotFound("JSON file does not exist in this path");

            List<Question> questions;
            try
            {
                // Read file content from route
                var jsonContent = await System.IO.File.ReadAllTextAsync(filePath);
                questions = JsonSerializer.Deserialize<List<Question>>(jsonContent);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred when reading file: {ex.Message}");
            }

            // Deserialization successfull? are there any questions in the file?
            if (questions == null || questions.Count == 0)
                return BadRequest("JSON has no valid questions");

            // Add questions and answers to db
            _context.Questions.AddRange(questions);
            await _context.SaveChangesAsync();

            return Ok("Questions inserted succesfully");
        }

        [HttpGet]
        [Route("quiz/getAll")]
        public async Task<IActionResult> GettAll()
        {
            var list = await _quizRepo.GetAllAsync();
            var listDto = list.Select(c => c.FromQToQDto());
            return Ok(listDto);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneById(int id)
        {
            var oneQuestion = await _quizRepo.GetOneAsync(id);
            if (oneQuestion == null) return NotFound("The question was not found");

            return Ok(oneQuestion.FromQToQDto());
        }

        [HttpPost]
        [Route("quiz/create")]
        public async Task<IActionResult> CreateQ([FromBody]CreateQuestionDto cqDto)
        {
            await _quizRepo.CreateAsync(cqDto.FromDtoToQ());
            return Ok(cqDto);
        }

        [HttpPut]
        [Route("quiz/update")]
        public async Task<IActionResult> UpdateQ(int id, [FromBody] UpdateQDto uqDto)
        {
            var question = await _quizRepo.UpdateAsync(id, uqDto);

            if (question == null) return BadRequest("The question/answer could not be updated");

            return Ok(question.FromQToQDto());
        }
    }
}
