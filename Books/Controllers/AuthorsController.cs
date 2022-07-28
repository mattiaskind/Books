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

        [HttpPost]
        public async Task<ActionResult<ViewAuthorDto>> CreateAuthor(CreateAuthorDto authorDto)
        {
            if (authorDto == null) return BadRequest();
            if (await _service.CheckIfAuthorNameExists(authorDto.Name))
            {
                ModelState.AddModelError(",", "En författare med samma namn finns redan");
                return StatusCode(422, ModelState);
            }

            var createdAuthor = await _service.CreateAuthorAsync(authorDto);

            return CreatedAtAction(nameof(GetAuthorById), new { Id = createdAuthor.Id }, createdAuthor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthorAsync(int id, CreateAuthorDto authorDto)
        {
            if (authorDto == null) return BadRequest();
            if (!(await _service.CheckIfAuthorExistsAsync(id))) return NotFound();

            var result = await _service.UpdateAuthorAsync(id, authorDto);
            if (!result)
            {
                ModelState.AddModelError(",", "Författaren kunde inte uppdateras");
                return StatusCode(500, ModelState);
            }
            else
            {
                return NoContent();
            }
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
