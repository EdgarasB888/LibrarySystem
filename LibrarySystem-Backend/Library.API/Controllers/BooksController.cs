using Library.Core.Services.BookService;
using Library.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet]
        [Route("{bookId}")]
        public async Task<ActionResult<Book>> GetBookById(int bookId)
        {
            var book = await _bookService.GetByIdAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook([FromBody] Book book)
        {
            if (book == null)
            {
                return BadRequest("Book cannot be null.");
            }

            var createdBook = await _bookService.CreateAsync(book);
            return CreatedAtAction(nameof(CreateBook), new { id = createdBook.BookId }, createdBook);
        }

        [HttpPut]
        [Route("{bookId}")]
        public async Task<ActionResult> UpdateBook(int bookId, [FromBody] Book book)
        {
            if (bookId != book.BookId)
            {
                return BadRequest("Book ID mismatch.");
            }

            await _bookService.UpdateAsync(book);
            return NoContent();
        }

        [HttpDelete]
        [Route("{bookId}")]
        public async Task<ActionResult> DeleteBook(int bookId)
        {
            var book = await _bookService.GetByIdAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }

            await _bookService.DeleteAsync(book);
            return NoContent();
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<IEnumerable<Book>>> SearchBooks([FromQuery] string query)
        {
            var books = await _bookService.SearchAsync(query);
            return Ok(books);
        }
    }
}
