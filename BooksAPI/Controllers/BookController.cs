using Book.DTOs;
using Book.Profiles;
using Books.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly BooksService _booksService;
        private readonly AuthorsService _authorsService;

        public BookController(ILogger<BookController> logger, BooksService booksService, AuthorsService authorsService)
        {
            _logger = logger;
            _booksService = booksService;
        }

        [HttpPost("upload-cover")]
        public ActionResult<ReponseCoverDTO> UploadCover([FromForm] IFormFile cover)
        {
            try
            {
                if (cover.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var filePath = Path.Combine(uploadsFolder, cover.FileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        cover.CopyTo(stream);
                    }

                    return Ok(new ReponseCoverDTO() { CoverPath = cover.FileName });
                }

                return BadRequest("Invalid file");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("get-cover/{coverFileName}")]
        public ActionResult GetCover(string coverFileName)
        {
            try
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
                var filePath = Path.Combine(uploadsFolder, coverFileName);

                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "image/jpeg"); // Adjust the content type based on your image format
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet(Name = "GetBooks")]
        public ActionResult<IEnumerable<BookDTO>> Get()
        {
            try
            {
                var result = new List<BookDTO>();

                var books = _booksService.GetBooks();

                foreach (var book in books)
                {
                    result.Add(BookProfile.Get(book));
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<BookDTO?> GetById(int id)
        {
            try
            {
                var result = BookProfile.Get(_booksService.GetById(id));

                if (result is null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            
        }

        [HttpPost]
        public ActionResult<BookDTO> Post(BookDTO book)
        {
            try
            {
                var result = _booksService.Insert(BookProfile.Get(book));

                return Ok(result);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult<Books.Data.Entities.Book> Delete(int id)
        {
            try
            {
                var bookFound = _booksService.GetById(id);

                if (bookFound != null)
                {
                    var result = _booksService.Delete(bookFound);

                    return Ok(result);
                }

                return NotFound();

            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut]
        public ActionResult<BookDTO> Put(BookDTO book)
        {
            try
            {
                var updatedBook = _booksService.Update(BookProfile.Get(book));

                return Ok(updatedBook);

            }catch(Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
