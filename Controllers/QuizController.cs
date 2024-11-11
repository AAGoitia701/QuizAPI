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
        private readonly IQuizRepository _quizRepo;
        public QuizController(IQuizRepository quiz)
        {
            _quizRepo = quiz;
        }

        [HttpGet]
        [Route("quiz/getAll")]
        public async Task<IActionResult> GettAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var list = await _quizRepo.GetAllAsync();
            var listDto = list.Select(c => c.FromQToQDto());
            return Ok(listDto);

        }
        [HttpGet]
        [Route("quiz/getOne/{id:int}")]
        public async Task<IActionResult> GetOneById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oneQuestion = await _quizRepo.GetOneAsync(id);
            if (oneQuestion == null) return NotFound("The question was not found");

            return Ok(oneQuestion.FromQToQDto());
        }

        [HttpPost]
        [Route("quiz/create")]
        public async Task<IActionResult> CreateQ([FromBody]CreateQuestionDto cqDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _quizRepo.CreateAsync(cqDto.FromDtoToQ());
            return Ok(cqDto);
        }

        [HttpPut]
        [Route("quiz/update/{id:int}")]
        public async Task<IActionResult> UpdateQ(int id, [FromBody] UpdateQDto uqDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var question = await _quizRepo.UpdateAsync(id, uqDto);

            if (question == null) return BadRequest("The question/answer could not be updated");

            return Ok(question.FromQToQDto());
        }

        [HttpDelete]
        [Route("quiz/delete/{id:int}")]
        public async Task<IActionResult> DeleteQ(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var q = await _quizRepo.DeleteAsync(id);
            if (q == null) return NotFound("The question/answer was not found");
            return Ok(q.FromQToQDto());
        }
    }
}
