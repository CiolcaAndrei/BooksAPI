using Books.Data.Entities;
using Books.Data.Interfaces;

namespace Books.Services.Services
{
    public class AuthorsService
    {
        private IRepository<Author> _authorRepository;

        public AuthorsService(IRepository<Author> authorsRepository)
        {
            _authorRepository = authorsRepository;
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _authorRepository.Get();
        }

        public Author Insert(Author author)
        {
            _authorRepository.Insert(author);
            _authorRepository.SaveChanges();

            return author;
        }

        public void Delete(Author author)
        {
            _authorRepository.Delete(author);
            _authorRepository.SaveChanges();
        }

        public Author? GetById(int id)
        {
            var result = _authorRepository.GetById(id);

            return result;
        }
    }
}
