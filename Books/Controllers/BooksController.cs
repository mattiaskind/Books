using Books.Dtos;
using Books.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private readonly BookService _service;

        public BooksController(BookService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<ViewBookDto>>> GetAllBooksAsync()
        {
            var books = await _service.GetAllBooksASync();
            if (books == null || books.Count == 0) return NotFound();
            return Ok(books);
        }

        [HttpGet("id")]
        public async Task<ActionResult<ViewBookDto>> GetBookByIdAsync(int id)
        {
            var book = await _service.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<ViewBookDto>> CreateBookAsync(CreateBookDto bookDto)
        {
            if (bookDto == null) return BadRequest(ModelState);
            if (await _service.CheckIfBookExistsAsync(bookDto.Title))
            {
                ModelState.AddModelError(",", "Boken finns redan");
                return StatusCode(422, ModelState);
            }

            ViewBookDto newBook = await _service.CreateBookAsync(bookDto);
            return CreatedAtAction(nameof(GetBookByIdAsync), new { Id = newBook.Id }, newBook);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBookAsync(int id, CreateBookDto book)
        {
            var result = await _service.ChangeBookAsync(id, book);
            if (!result)
            {
                ModelState.AddModelError(",", "Något gick fel, boken kunde inte ändras");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            if (id == 0) return BadRequest();

            var result = await _service.DeleteBookAsync(id);
            if (!result)
            {
                ModelState.AddModelError(",", "Något gick fel när boken skulle tas bort");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
