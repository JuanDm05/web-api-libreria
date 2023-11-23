using libreriaa_JADM.Data.Services;
using libreriaa_JADM.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace libreriaa_JADM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private AuthorService _authorService;

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpPost("add-author")]
        public IActionResult AddAuthor([FromBody] AuthorVM author)
        {
            _authorService.AddAuthor(author);
            return Ok();
        }
        [HttpGet("get-author-with-books-by-id/{id}")]
        
        public IActionResult GetAuthorWithBooks(int id)
        {
        var response = _authorService.GetAuthorWithBooks(id);
            return Ok(response);
        }

    }
}
