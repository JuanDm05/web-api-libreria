using libreriaa_JADM.Data.Services;
using libreriaa_JADM.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace libreriaa_JADM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet("get-all-books")]
        public IActionResult GetAllBooks()
        {
            var allbooks = _bookService.GetAllBks();
            return Ok(allbooks);
        }

        [HttpGet("get-books-by-id/{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _bookService.GetBookById(id);
            return Ok(book);
        }



        [HttpPost("add-book-with-authors")]
        public IActionResult AddBook([FromBody] BookVM book)
        {
            _bookService.AddBookWithAuthors(book);
            return Ok();
        }
        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] BookVM book)
        {
            var updateBook = _bookService.UpdateBookById(id, book);
            return Ok(updateBook);
        }
        [HttpDelete("update-book-by-id/{id}")]

        public IActionResult DeleteBookById(int id)
        {
            _bookService.DeleteBookById(id);
            return Ok();    
        }
    }
}
