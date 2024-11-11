using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Data;
using QuizAPI.Models;
using System.Text.Json;

namespace QuizAPI.Controllers
{
    public class JsonController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JsonController(ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
