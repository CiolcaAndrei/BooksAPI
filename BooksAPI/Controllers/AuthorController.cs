using Books.Data.Entities;
using Books.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly AuthorsService _authorService;

        public AuthorController(ILogger<AuthorController> logger, AuthorsService authorService)
        {
            _logger = logger;
            _authorService = authorService;
        }

        [HttpGet]
        public IEnumerable<Author> Get()
        {
            var result = _authorService.GetAuthors();

            return result;
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var authorFound = _authorService.GetById(id);

            if (authorFound != null)
            {
                _authorService.Delete(authorFound);
            }
        }

        [HttpPost]
        public Author Post(Author book)
        {
            _authorService.Insert(book);

            return book;
        }
    }
}
