using Books.Dtos;
using Books.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _service;

        public AuthorsController(AuthorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ViewAuthorDto>>> GetAllAuthorsAsync()
        {
            var result = await _service.GetAllAuthorsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewAuthorDto>> GetAuthorById(int id)
        {
            var result = await _service.GetAuthorById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAuthor(int id)
        {
            if (id == 0) return BadRequest();
            int numberOfBooks = await _service.GetNumberOfBooks(id);
            if (numberOfBooks > 0)
            {
                ModelState.AddModelError(",", "Det finns böcker med aktuell författare, ta först bort böckerna");
                return StatusCode(422, ModelState);
            }
            var result = await _service.DeleteAuthorAsync(id);
            if (!result)
            {
                ModelState.AddModelError(",", "Något gick fel när boken skulle tas bort");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
